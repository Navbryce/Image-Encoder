import io
from PIL import Image

def bitxor(bit_one, bit_two):
    """
    returns a string
    """
    bit_one = int(bit_one)
    bit_two = int(bit_two)
    if bit_one != bit_two: # one must be true
        new_bit = "1"
    else:
        new_bit = "0"
    return new_bit


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

def bytes_to_image(bytes_array, file_path):
    """writes bytes array to image. bytes array must be an array of ints (not binary)"""
    image_file = Image.open(io.BytesIO(bytes_array))
    image_file.save(file_path)

def convert_byte_array (byte_array, length): # converts byte array to binary
    binary = []
    for byte in byte_array:
        binary.append(convert_byte_to_binary(byte, length))
    return binary


def convert_byte_to_binary(byte, length):
    length = '{}'.format(length) # makes length a string
    format = '{0:0' + length + 'b}'
    return format.format(byte)


def convert_from_binary(binary):
    return int(binary, 2) # binary has a base of 2
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


def convert_bytes_to_string(bytes_array):
    """
    bytes_array : should be a bytearray object with ints as elements
    """
    return bytes_array.decode("utf-8")


def convert_string_to_bytes(string):
    return bytearray(string, "utf-8")

def convert_from_binary_array(binary_array):
    """Converts an array of binary value bytes to a bytearray"""
    byte_array = bytearray() # CAN'T BE A NORMAL ARRAY. MUST BE A BYTE ARRAY
    for binary in binary_array:
        int_object = convert_from_binary(binary)
        byte_array.append(int_object)
    return byte_array

def get_bit_from_byte (byte, bit_index):
    """
    Get the bit at bit_index in the byte
    Assumes the byte is BINARY
    """
    return byte[bit_index]


def get_image_file(file_path):
    with open(file_path, "rb") as image_file_locked:
        file_string = image_file_locked.read()
    return file_string

def XOR_on_bits_reverse(encrypted_bits, original_comparison_bits, run_operation_check = None, comparison_offset = 0):
    """
    performs REVERSE XOR operation on encrypted_bits.
    ASSUMES encrypted_bits has been encrypted with XOR with original_comparison_bits as the comparison
    ASSUMES the same run_operation_check was used in the original XOR
    ASSUMES original_comparison_bits  was used in the XOR operation used to encrypt the bits
    coparison_offset - the number of bits to offset for the starting comparison bit. Used when decrypting part of an encrypted string and the encoding bits are not at the start
    """
    result = []
    if comparison_offset >= len(original_comparison_bits):
        comparison_bit_index = comparison_offset % len(original_comparison_bits) # modulus because the comparison off_set MIGHT be greater than the length of the comparison array if the message is longer than the secret_key
    else:
        comparison_bit_index = comparison_offset
    for bit_index in range(0, len(encrypted_bits)):
        encrypt_bit = int(encrypted_bits[bit_index])
        comparison_bit = int(original_comparison_bits[comparison_bit_index])
        if run_operation_check is None or run_operation_check(None, comparison_bit): # only run the bitxor operation if no check function has been sent or it returns true
            original_bit = bitxor_reverse(encrypt_bit, comparison_bit)
            result.append(str(original_bit))
        else:
            result.append(str(encrypt_bit)) # don't change the bit because XOR was never run on it

        comparison_bit_index += 1
        if comparison_bit_index == len(original_comparison_bits): # keep looping around the comparison bit array if the bits array is larger
            comparison_bit_index = 0
    return result
