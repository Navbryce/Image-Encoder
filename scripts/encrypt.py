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

    def encrypt (self, secret_key_string):
        secret_key = EncryptString(secret_key_string)
        secret_key_bits = secret_key.XOR_on_bits([1]) # get secret key bits with x or bits

        run_operation_check = None # not necessary
        # modify data (encrypt)
        self.bits = self.XOR_on_bits(secret_key_bits, run_operation_check)
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
        does NOT update the string
        """
        # update all data holders
        self.binary_array = self.create_binary_byte_array_from_bits()
        self.bytes = self.create_bytes_array_from_binary_array()

    def update_string(self):
        self.string = self.create_string_from_bytes()

    def XOR_on_bits(self, comparison_bits, run_operation_check = None):
        """
        performs XOR operation on all bits in self.bits. Each bit is compared to the single comparison bits. Returns result
        XOR - Returns true ONLY if one bit is true. If both are true, returns false. If neither are true, returns false
        run_operation_check - lamda function with two parameters, the main_bit and the comparison_bit. If true, it runs the bitxor operation
        """
        result = []
        comparison_bit_index = 0
        for bit_index in range(0, len(self.bits)):
            main_bit = int(self.bits[bit_index])
            comparison_bit = int(comparison_bits[comparison_bit_index])
            if run_operation_check is None or run_operation_check(main_bit, comparison_bit): # only run the bitxor operation if no check function has been sent or it returns true
                new_bit = EncryptString.bitxor(main_bit, comparison_bit)
                result.append(new_bit)
            else:
                result.append(str(main_bit)) # don't change the bit

            comparison_bit_index += 1
            if comparison_bit_index == len(comparison_bits): # keep looping around the comparison bit array if the bits array is larger
                comparison_bit_index = 0

        return result

    # static methods
    @staticmethod
    def decrypt(encrypted_bits, secret_key_string):
        """
        ASSUMES encrypted_bits has been encrypted with the secret_key string
        """
        original_xor_check = None # not necessary
        secret_key = EncryptString(secret_key_string)
        secret_key_bits = secret_key.XOR_on_bits([1]) # get secret key bits with x or bits

        decrypted_bits = EncryptString.XOR_on_bits_reverse(encrypted_bits, secret_key_bits, original_xor_check)
        decrypted_bytes_in_binary = EncryptString.convert_bits_array(decrypted_bits, EncryptString.byte_size)
        decrypted_int_bytes = StegImage.convert_from_binary_array(decrypted_bytes_in_binary)
        string = EncryptString.convert_bytes_to_string(decrypted_int_bytes)
        return string

    @staticmethod
    def bitxor(bit_one, bit_two):
        """
        returns a string
        """
        bit_one = int(bit_one)
        bit_two = int(bit_two)
        if bit_one != bit_two and (bit_two is 1 or bit_one is 1):
            new_bit = "1"
        else:
            new_bit = "0"
        return new_bit

    @staticmethod
    def bitxor_reverse(bitxor_bit, original_bit):
        """
        bitxor_bit - the resulting bit from the bitxor operation
        original_bit - a bit you KNOW was used in the original operation
        returns a string that is the other bit from the original comparison operation
        """
        bitxor_bit = int(bitxor_bit)
        original_bit = int(original_bit)

        if bitxor_bit is 1:
            if original_bit is 1:
                other_bit = 0
            else:
                other_bit = 1
        else:
            other_bit = original_bit # if bitxor is false, it means both bits had the same value
        return other_bit

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

    @staticmethod
    def XOR_on_bits_reverse(encrypted_bits, original_comparison_bits, run_operation_check = None):
        """
        performs REVERSE XOR operation on encrypted_bits.
        ASSUMES encrypted_bits has been encrypted with XOR with original_comparison_bits as the comparison
        ASSUMES the same run_operation_check was used in the original XOR
        ASSUMES original_comparison_bits  was used in the XOR operation used to encrypt the bits
        """
        result = []
        comparison_bit_index = 0
        for bit_index in range(0, len(encrypted_bits)):
            encrypt_bit = int(encrypted_bits[bit_index])
            comparison_bit = int(original_comparison_bits[comparison_bit_index])
            if run_operation_check is None or run_operation_check(None, comparison_bit): # only run the bitxor operation if no check function has been sent or it returns true
                original_bit = EncryptString.bitxor_reverse(encrypt_bit, comparison_bit)
                result.append(str(original_bit))
            else:
                result.append(str(encrypt_bit)) # don't change the bit because XOR was never run on it

            comparison_bit_index += 1
            if comparison_bit_index == len(original_comparison_bits): # keep looping around the comparison bit array if the bits array is larger
                comparison_bit_index = 0
        return result

"""Driver/Tester"""
if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    string = "test test test"
    encrypt = EncryptString(string)
    secret_key = "bbaaaa"
    encrypt.encrypt(secret_key)
    encrypted_bits = encrypt.bits
    decrypt_string = EncryptString.decrypt(encrypted_bits, "aaaaa")
    if decrypt_string != string:
        print("Decryption did not work with string, %s, and secret key, %s. The output of decryption was: %s"%(string, secret_key, decrypt_string))
    else:
        print("Decryption of, %s, worked with secret_key, %s."%(string, secret_key))
