import os

import io
import random
from PIL import Image
from encrypt import EncryptString

class StegImage(object):
    # static variables
    info = {
        "bmp": {
            "offset": 54 # the index for the first byte with image information
        }
    }
    binary_size = 8

    # public methods
    def __init__(self, image_file_string, file_type):
        """
        image_file_string : image file stread from .read()
        file_type : file type (don't include period). used to determine offset
        """
        self.binary_array = [] # is set in the self.update_binary_bytes
        self.file_type = file_type
        self.offset = self.info[self.file_type]["offset"] # the number of bytes to offset to get to the actual image byets
        self.bytes = bytearray(image_file_string) # bytes array is formatted with regular numbers (ints)
        self.update_binary_bytes() # self.binary_array is formated with binary. stores it in self.binary_array

        # self.number_of_pixels SHOULD be a whole number
        self.number_of_pixels = (len(self.bytes) - self.offset) / 3 # (-self.offset) because the first few bytes aren't pixels. each pixel has 3 bytes associated with it so divide the number of bytes by 3 to get the number of pixels

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
        # all of the bits have been updated

    def embed_message(self, message, secret_key, random_seed):
        """embeds a message into the image. Does not update the image file. Updates the bits stored in the instance of this object"""
        encrypt = EncryptString(string = message)
        encrypted_bits = encrypt.encrypt(secret_key).bits
        self.embed_bits(encrypted_bits, 0, random_seed) # 0 because only modify the red plane



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
        self.binary_array = StegImage.convert_byte_array(self.bytes, self.binary_size)

    def update_bytes_array(self):
        """updates the bytes array based on binary bytes. reconstructs bytes from binary_array"""
        self.bytes = StegImage.convert_from_binary_array(self.binary_array)

    def write_bytes_to_image(self, file_path):
        """writes self.bytes to an image. probably should run self.update_bytes_array() before calling this method"""
        StegImage.bytes_to_image(self.bytes, file_path)

    # static methods
    @staticmethod
    def convert_byte_array (byte_array, length): # converts byte array to binary
        binary = []
        for byte in byte_array:
            binary.append(StegImage.convert_byte_to_binary(byte, length))
        return binary

    @staticmethod
    def convert_byte_to_binary(byte, length):
        length = '{}'.format(length) # makes length a string
        format = '{0:0' + length + 'b}'
        return format.format(byte)

    @staticmethod
    def convert_from_binary(binary):
        return int(binary, 2) # binary has a base of 2

    @staticmethod
    def convert_from_binary_array(binary_array):
        """Converts an array of binary value bytes to a bytearray"""
        byte_array = bytearray() # CAN'T BE A NORMAL ARRAY. MUST BE A BYTE ARRAY
        for binary in binary_array:
            int_object = StegImage.convert_from_binary(binary)
            byte_array.append(int_object)
        return byte_array

    @staticmethod
    def get_image_file(file_path):
        with open(file_path, "rb") as image_file_locked:
            file_string = image_file_locked.read()
        return file_string

    @staticmethod
    def bytes_to_image(bytes_array, file_path):
        """writes bytes array to image. bytes array must be an array of ints (not binary)"""
        image_file = Image.open(io.BytesIO(bytes_array))
        image_file.save(file_path)


"""Driver/Tester"""
if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    images_root = "C:/Users/navba/Downloads/DevenirProjects/Image-Encoder/images/"
    image_name ="dog.bmp"
    image_file_string = StegImage.get_image_file(images_root + image_name)
    image = StegImage(image_file_string, "bmp")
    image.embed_message("hello", "test", "test")
    # after convering bytes to binary and binary back to bytes, try to recreate the image
    image.write_bytes_to_image(images_root + "recreated_" + image_name)
