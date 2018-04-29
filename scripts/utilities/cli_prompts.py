import math
from utilities import data_manipulation

def get_image_file_string():
    """
    Prompts user for path. Returns image file string (a string representation of a COMPLETE file). will keep prompting until user gives real image
    """
    image_file_string = None
    while image_file_string is None:
        path = input("Please enter a complete path to the image: ")
        try:
            image_file_string = data_manipulation.get_image_file(path)
        except:
            image_file_string = None
            print("The you entered was not correct or an error occurred while trying to load in the image...")
    return image_file_string

def get_image_type():
    """ Returns an image type"""
    allowed_image_types = { # serves as a hashmap for acceptable image types
        "bmp": True
    }

    image_type = None
    while image_type is None:
        type = input("Enter the image type (bmp, png, jpg): ")
        if type in allowed_image_types:
            image_type = type
    return type

def get_integer(prompt_message):
    """can be used to get an integer from a user"""
    integer = None
    while integer is None:
        try:
            integer = int(input(prompt_message))
        except: # an exception is thrown when the user does not enter an int (like a string)
            print("You did not enter a valid integer. Please try again.")
    return integer
    
def get_message_length():
    """returns message_length. will return math.inf if the user enters a negative value"""
    message_length = get_integer("Mesesage length (for better decryption results). Enter -1 if you don't know the message length: ")
    if message_length < 0:
        message_length = math.inf
    return message_length

def get_message_keys():
    """ prompts the user for an encryption key and random seed """
    encryption_key = input("Enter a key to encrypt your message (the longer and more random, the better): ")
    random_seed = input("Enter the steg random seed. This is also used for encryption. The more random, the better: ")
    return {
        "encryption_key": encryption_key,
        "random_seed": random_seed
    }
