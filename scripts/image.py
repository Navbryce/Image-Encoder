import sys
import random
import traceback
from utilities import data_manipulation
from encrypt import EncryptString

class StegImage(object):
    # static variables
    info = {
        "bmp": {
            "offset": 54 # the index for the first byte with image information
        }
    }
    binary_size = 8
    color_offset = 0

    # public methods
    def __init__(self, file_type, image_file_string = None, image_path = None):
        """
        image_file_string : image file stread from .read() . NOT the image path
        file_type : file type (don't include period). used to determine offset
        """
        error = False
        if image_path is not None:
            image_file_string = data_manipulation.get_image_file(image_path)
        elif image_file_string is None:
            error = True
            print("An image_path has not been set NOR has an image_file_string (string representative of a file). No constructors have been used. This will not work")
        if error is False:
            self.binary_array = [] # is set in the self.update_binary_bytes
            self.file_type = file_type
            self.offset = self.info[self.file_type]["offset"] # the number of bytes to offset to get to the actual image byets
            self.bytes = bytearray(image_file_string) # bytes array is formatted with regular numbers (ints)
            self.update_binary_bytes() # self.binary_array is formated with binary. stores it in self.binary_array

            # self.number_of_pixels SHOULD be a whole number
            self.number_of_pixels = (len(self.bytes) - self.offset) / 3 # (-self.offset) because the first few bytes aren't pixels. each pixel has 3 bytes associated with it so divide the number of bytes by 3 to get the number of pixels

    def decode (self, secret_key, random_seed, message_length):
        """
        Attempts to decode a message hidden in the image
        The secret_key and random_seed MUST be the same as the ones used to embed the message initially
        """
        # seed the random number generator with the seed used to embed
        random.seed(random_seed)
        bytes_visited = {} # a dictionary of the unique bytes already visited
        color_offset = StegImage.color_offset # the color plane where the message exists
        recent_bits = [] # an array. each element is a single bit
        while len(bytes_visited) < message_length * self.binary_size:   # will try to decode one letter at a time until an error is thrown or it reaches the end of the image. (the algo has no idea when the message stops)
            index_of_byte = None
            while (index_of_byte is None or index_of_byte in bytes_visited): # if the byte is visited twice, in the embed algo, it just skips it the second time and moves on, so do the same when decoding
                index_of_byte = random.randint(self.offset, self.number_of_pixels * 3)
                index_of_byte += color_offset
            bytes_visited[index_of_byte] = True
            byte = self.binary_array[index_of_byte]
            bit = data_manipulation.get_bit_from_byte(byte, self.binary_size - 1) # get the last bit of the byte
            recent_bits.append(bit)
        # decrypt
        encrypt_object = EncryptString(single_bits=recent_bits, secret_key=secret_key)
        encrypt_string = encrypt_object.string
        message = encrypt_string
        recent_bits = [] # reset recent bits
        return message

    def embed_bits(self, bits_to_embed, color_offset, random_seed):
        """
        color_offset - should be 0 (red), 1 (green), or 2 (blue)
        creates stego image with message embedded.
        random_seed - required to determien positions randomly (should be a secret key; hold onto it to decode the image)
        """
        bit_embedded = {} # hash map. if the bit is already storing encrypted data, then it will be in the dictionary
        random.seed(random_seed) # not really random. random_seed will be needed to decrypt. used to determine positions of pixels
        for message_bit in bits_to_embed:
            index_of_byte = None
            while (index_of_byte is None or index_of_byte in bit_embedded): # select a byte. if a bit has already been embedded in the byte, try again
                index_of_byte = random.randint(self.offset, self.number_of_pixels * 3) # pick a byte between the lowest byte (because of codec) and highest byte for picture information
                # index is set to the first byte of a pixel (pixels come in groups of three)
                index_of_byte += color_offset # select a color within a certain plane.
            bit_embedded[index_of_byte] = True
            self.lsb_update(index_of_byte, message_bit)
        self.update_bytes_array() # apply the changes to the bytes array AKA update all of the whole number bytes. without this, it wouldn't work


        # all of the bits have been updated

    def embed_message(self, message, secret_key, random_seed):
        """embeds a message into the image. Does not update the image file. Updates the bits stored in the instance of this object"""
        encrypt = EncryptString(string = message)
        encrypt.encrypt(secret_key)
        encrypted_bits = encrypt.bits
        self.embed_bits(encrypted_bits, self.color_offset, random_seed) # 0 (self.color_offset) because only modify the red plane

    def lsb_update(self, index, new_lsb):
        """
        changes the least significant bit of an image. tbh, this could be done with normal bytes by just incrementing the byte by 1 (or potentially substracting by 1/ keeping it the same)
        index - the index of the byte
        """
        self.set_bit(index, self.binary_size - 1, new_lsb)

    def print_binary_array(self):
        for byte in self.binary_array:
            print(byte)

    def print_normal_array(self):
        for byte in self.bytes:
            print(byte)

    def set_bit(self, index_of_byte, index_of_bit, new_value):
        """
        index_of_byte - index of byte in array
        index_of_bit - index of the bit in the byte
        new_value - the new value at the index of the bit (0 or 1)
        """
        if index_of_bit >= self.binary_size:
            print("You tried to modify a byte at %d index. This cannot be done. The maximum index is %d."%(index_of_bit, self.binary_size - 1))
        else:
            new_value = str(new_value)
            byte = self.binary_array[index_of_byte]
            new_byte = byte[0:index_of_bit] + new_value
            if index_of_bit < self.binary_size - 1: # you aren't changing the final bit in the byte
                new_byte += byte[index_of_bit + 1:]
            #apply changes
            self.binary_array[index_of_byte] = new_byte
    def update_binary_bytes(self):
        """Updates the binary_array array based on the bytes private variable"""
        self.binary_array = data_manipulation.convert_byte_array(self.bytes, self.binary_size)

    def update_bytes_array(self):
        """updates the bytes array based on binary bytes. reconstructs bytes from binary_array"""
        self.bytes = data_manipulation.convert_from_binary_array(self.binary_array)

    def write_bytes_to_image(self, file_path):
        """writes self.bytes to an image. probably should run self.update_bytes_array() before calling this method"""
        data_manipulation.bytes_to_image(self.bytes, file_path)

"""Driver/Tester"""
if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    images_root = "C:/Users/navba/Downloads/DevenirProjectsA/Image-Encoder/images/"
    image_name ="dog.bmp"
    new_name = images_root + "recreated_" + image_name
    # Important variables
    message = "Now we are engaged in a great civil war, testing whether that nation, or any nation so conceived and so dedicated, can long endure. We are met on a great battle-field of that war. We have come to dedicate a portion of that field, as a final resting place for those who here gave their lives that that nation might live. It is altogether fitting and proper that we should do this."
    secret_key = "test"
    random_seed = "random"

    """
    #embed test
    image_file_string = data_manipulation.get_image_file(images_root + image_name)
    image = StegImage("bmp", image_file_string = image_file_string)
    image.embed_message(message, secret_key, random_seed)
    # write to new image
    image.write_bytes_to_image(new_name)
    """

    # try to get message from new image
    new_image = StegImage("bmp", image_path=new_name)
    print(new_image.decode(secret_key, random_seed, len(message)))
