from utilities import cli_prompts
from image import StegImage

def create_steg():
    image_type = cli_prompts.get_image_type()
    image_file_string = cli_prompts.get_image_file_string()
    encrypt_image = StegImage(image_type, image_file_string = image_file_string)
    message_parameters = get_message_parameters()
    encrypt_image.embed_message(message_parameters["message"], message_parameters["secret_key"], message_parameters["random_seed"])
    encrypt_image.write_bytes_to_image(get_new_image_path())

def get_message_parameters():
    message = input("Enter your message: ")
    keys = cli_prompts.get_message_keys()
    encryption_key = keys["encryption_key"]
    random_seed = keys["random_seed"]
    return {
        "message": message,
        "secret_key": encryption_key,
        "random_seed": random_seed
    }
def get_new_image_path():
    new_path = input("Enter the path (and the name) to the new image. Include the '.bmp': ")
    return new_path

if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    create_steg()
