import traceback
from utilities import cli_prompts
from image import StegImage

def decode_steg():
    print("Decode image: ")
    image_type = cli_prompts.get_image_type()
    image_file_string = cli_prompts.get_image_file_string() # a string representation of the entire file
    encryption_keys = cli_prompts.get_message_keys()

    steg_image = StegImage(image_type, image_file_string=image_file_string)

    # OPTIONAL PARAMETER: message_length which gives better decoding results. tells it EXACTLY where to stop
    message_length = cli_prompts.get_message_length()

    try:
        message = steg_image.decode(encryption_keys["encryption_key"], encryption_keys["random_seed"], message_length=message_length)
        print("The decoded message is: " + message)
    except:
        print("An error occurred while decoding. It's possible you entered the wrong decryption keys.")
        traceback.print_exc()


if __name__ == '__main__': # if someone directly ran this script rather than importing it, run the code below
    decode_steg()
