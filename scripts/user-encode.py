from utilities import data_manipulation
from image import StegImage

def create_steg():
    image_type = get_image_type()
    image_file_string = get_image_file_string()
    encrypt_image = StegImage(image_type, image_file_string = image_file_string)
    message_parameters = get_message_parameters()
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
def get_message_parameters():
    message = input("Enter your message: ")
    encryption_key = input("Enter a key to encrypt your message (the longer and more random, the better): ")
    random_seed = input("Enter the steg random seed. This is also used for encryption. The more random, the better: ")
    return {
        "message": message,
        "encryption_key": encryption_key,
        "random_seed": random_seed
    }

if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    create_steg()
