import os
import io
from PIL import Image

class StegImage(object):
    def __init__(self, image_file):
        self.bytes = bytearray(image_file) # bytes array is formatted with regular numbers (ints)
        self.update_binary_bytes() #self.binary_array is formated with binary

    def print_binary_array(self):
        for byte in self.binary_array:
            print(byte)

    def print_normal_array(self):
        for byte in self.bytes:
            print(byte)

    def update_binary_bytes(self):
        """Updates the binary_array array based on the bytes private variable"""
        self.binary_array = StegImage.convert_byte_array(self.bytes, 8)

    def update_bytes_array(self):
        """updates the bytes array based on binary bytes. reconstructs bytes from binary_array"""
        self.bytes = StegImage.convert_from_binary_array(self.binary_array)

    def write_bytes_to_image(self, file_path):
        """writes self.bytes to an image. probably should run self.update_bytes_array() before calling this method"""
        StegImage.bytes_to_image(self.bytes, file_path)

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
            file = image_file_locked.read()
        return file

    @staticmethod
    def bytes_to_image(bytes_array, file_path):
        """writes bytes array to image. bytes array must be an array of ints (not binary)"""
        image_file = Image.open(io.BytesIO(bytes_array))
        image_file.save(file_path)


"""Driver/Tester"""
if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    images_root = "A:/DevenirProjectsA/Image-Encoder/images/"
    image_name ="dog.jpg"
    image_file = StegImage.get_image_file(images_root + image_name)
    image = StegImage(image_file)

    image.update_bytes_array() # bytes array is filled with ints. reconstructs bytes arrays based on binary array
    # after convering bytes to binary and binary back to bytes, try to recreate the image
    image.write_bytes_to_image(images_root + "recreated_" + image_name)
