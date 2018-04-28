from utilities import data_manipulation

class EncryptString(object):
    # uses utf-8
    byte_size = 8

    # public methods
    def __init__(self, string = None, single_bits = None, secret_key = None):
        """
        3 constructors in 1
        - 1 string (updates all data structures based on string)
        - just a single_bits array (assumes data is unencrypted; will create string from bits)
        - single-bits array AND decrypt_string (will try to unencrypt data and will update all other data structures)
        """
        if string is not None or (single_bits is not None and secret_key is not None): # creating from a string OR from a decryption
            if string is not None:
                self.string = string
            else:
                self.string = EncryptString.decrypt(single_bits, secret_key)
            self.bytes = data_manipulation.convert_string_to_bytes(self.string)
            self.binary_array = data_manipulation.convert_byte_array(self.bytes, self.byte_size)
            self.bits = self.create_bit_array()
        elif single_bits is not None: # creating from an array of single bits. Assumes byts are NOT encrypted
            self.bits = single_bits
            self.string = self.create_string_from_bytes()
            self.update_all_structures()
        else:
            print("ERROR: The constructor for Encrypt String did not work because the right parameters were not supplied")

    def create_binary_byte_array_from_bits(self):
        return data_manipulation.convert_bits_array(self.bits, self.byte_size)

    def create_bit_array(self):
        """create an array where each element is a single bit"""
        return data_manipulation.convert_binary_array_to_single_array(self.binary_array, self.byte_size)

    def create_bytes_array_from_binary_array(self):
        """returns the bytes array based on binary bytes array. reconstructs bytes from binary_array"""
        return data_manipulation.convert_from_binary_array(self.binary_array)

    def create_string_from_bytes(self):
        """returns a string based off the bytes array"""
        return data_manipulation.convert_bytes_to_string(self.bytes)

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
                new_bit = data_manipulation.bitxor(main_bit, comparison_bit)
                result.append(new_bit)
            else:
                result.append(str(main_bit)) # don't change the bit

            comparison_bit_index += 1
            if comparison_bit_index == len(comparison_bits): # keep looping around the comparison bit array if the bits array is larger
                comparison_bit_index = 0

        return result

    # static method
    @staticmethod
    def decrypt(encrypted_bits, secret_key_string, character_offset=0):
        """
        ASSUMES encrypted_bits has been encrypted with the secret_key string
        character_number - the number of characters BEFORE this character (or series of characters) in the message. For example, you might only want to decode the 5th letter of an encoded string
        """
        original_xor_check = None # not necessary
        secret_key = EncryptString(secret_key_string)
        secret_key_bits = secret_key.XOR_on_bits([1]) # get secret key bits with x or bits

        offset = character_offset * EncryptString.byte_size # the number of bits to offset (each character is byte with the byte size of bits)
        decrypted_bits = data_manipulation.XOR_on_bits_reverse(encrypted_bits, secret_key_bits, original_xor_check, comparison_offset = offset)
        decrypted_bytes_in_binary = data_manipulation.convert_bits_array(decrypted_bits, EncryptString.byte_size)
        decrypted_int_bytes = data_manipulation.convert_from_binary_array(decrypted_bytes_in_binary)
        string = data_manipulation.convert_bytes_to_string(decrypted_int_bytes)
        return string
"""Driver/Tester"""
if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    string = "test"
    encrypt = EncryptString(string = string)
    secret_key = "secret key"
    encrypt.encrypt(secret_key)
    encrypted_bits = encrypt.bits
    decrypt_string = EncryptString.decrypt(encrypted_bits, secret_key)
    if decrypt_string != string:
        print("Decryption did not work with string, %s, and secret key, %s. The output of decryption was: %s"%(string, secret_key, decrypt_string))
    else:
        print("Decryption of, %s, worked with secret_key, %s."%(string, secret_key))
