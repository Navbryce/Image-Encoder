import os
import io
from PIL import Image

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
        self.update_binary_bytes() #self.binary_array is formated with binary. stores it in self.binary_array

        for index in range(self.offset, len(self.binary_array)):
            self.update_lsb(index, 1)


    def update_lsb (self, index, new_lsb):
        """
        changes the least significant bit of an image. tbh, this could be done with normal bytes by just incrementing the byte by 1 (or potentially substracting by 1/ keeping it the same)
        """
        new_lsb = str(new_lsb)
        byte = self.binary_array[index]
        byte = byte[0:self.binary_size - 1] + new_lsb

        #apply changes
        self.binary_array[index] = byte

    def print_binary_array(self):
        for byte in self.binary_array:
            print(byte)

    def print_normal_array(self):
        for byte in self.bytes:
            print(byte)

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
        """Converts an array of binary values to a bytearray"""
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
    images_root = "A:/DevenirProjectsA/Image-Encoder/images/"
    image_name ="dog.bmp"
    image_file_string = StegImage.get_image_file(images_root + image_name)
    image = StegImage(image_file_string, "bmp")

    image.update_bytes_array() # bytes array is filled with ints. reconstructs bytes arrays based on binary array
    # after convering bytes to binary and binary back to bytes, try to recreate the image
    image.write_bytes_to_image(images_root + "recreated_" + image_name)
