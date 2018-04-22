from image import StegImage
class EncryptString(object):
    # uses utf-8
    byte_size = 8

    # public methods
    def __init__(self, string):
        self.string = string
        self.bytes = EncryptString.convert_string_to_bytes(string)
        self.binary_array = StegImage.convert_byte_array(self.bytes, self.byte_size)
        self.bits = self.create_bit_array()

    def create_binary_byte_array_from_bits(self):
        return EncryptString.convert_bits_array(self.bits, self.byte_size)

    def create_bit_array(self):
        """create an array where each element is a single bit"""
        return EncryptString.convert_binary_array_to_single_array(self.binary_array, self.byte_size)

    def create_bytes_array_from_binary_array(self):
        """returns the bytes array based on binary bytes array. reconstructs bytes from binary_array"""
        return StegImage.convert_from_binary_array(self.binary_array)

    def create_string_from_bytes(self):
        """returns a string based off the bytes array"""
        return EncryptString.convert_bytes_to_string(self.bytes)

    def encrypt (self):
        # modify data
        self.bits = self.XOR_on_bits(1)
        # save changes
        self.update_all_structures()

    def print_binary_array(self):
        for byte in self.binary_array:
            print(byte)

    def print_normal_array(self):
        for byte in self.bytes:
            print(byte)

    def update_all_structures(self):
        """
        will update all the data holders based on the lowest level data holder (the individual bits array)
        should be called when the bits array changes
        """
        # update all data holders
        self.binary_array = self.create_binary_byte_array_from_bits()
        self.bytes = self.create_bytes_array_from_binary_array()
        self.string = self.create_string_from_bytes()

    def XOR_on_bits(self, comparison_bit):
        """
        performs XOR operation on all bits in self.bits. Each bit is compared to the single comparison bits. Returns result
        XOR - Returns true ONLY if one bit is true. If both are true, returns false. If neither are true, returns false
        """
        result = []
        compare_bit_is_true = comparison_bit is 1
        for bit_index in range(0, len(self.bits)):
            bit = int(self.bits[bit_index])
            if bit != comparison_bit and (compare_bit_is_true or bit is 1):
                new_bit = "1"
            else:
                new_bit = "0"
            result.append(new_bit)

        return result


    # static methods
    @staticmethod
    def convert_bits_array(bits_array, byte_length):
        """
        return an array of bytes (in binary strings) created from an array where each element is a single bit
        """
        bytes_array = []
        if len(bits_array) % byte_length != 0:
            print("The number of bits, %d, does not divide evenly with byte_length, %d. Something is wrong."%(len(bits_array), byte_length))
        else:
            number_of_bytes = int(len(bits_array)/byte_length)
            for counter in range(0, number_of_bytes):
                offset = counter * byte_length
                byte = ""
                for index in range(offset, offset + byte_length):
                    bit = bits_array[index]
                    byte += bit
                bytes_array.append(byte)
        return bytes_array


    @staticmethod
    def convert_binary_array_to_single_array(binary_array, byte_length):
        """
        converts an array of binary bytes into a 1D array where each element is just one bit. so a byte of length 8 would become 8 bits
        NOTE: Assumes all bytes are byte_length
        """
        array = []
        for binary_byte in binary_array:
            for index in range(0, byte_length):
                bit = binary_byte[index]
                array.append(bit)
        return array

    @staticmethod
    def convert_bytes_to_string(bytes_array):
        """
        bytes_array : should be a bytearray object with ints as elements
        """
        return bytes_array.decode("utf-8")

    @staticmethod
    def convert_string_to_bytes(string):
        return bytearray(string, "utf-8")

"""Driver/Tester"""
if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    encrypt = EncryptString("test")
    encrypt.encrypt()
