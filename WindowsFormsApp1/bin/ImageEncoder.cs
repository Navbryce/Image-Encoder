using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsFormsApp1.bin;
using WindowsFormsApp1.bin.Utilities;

public class ImageEncoder
{
    // Static variables
    public static int byteSize = 8;
    public static int colorOffset = 0; // set the color plane that should be modified for encryption
    public static Dictionary<string, int> imageOffset = new Dictionary<string, int>();


    // Public variables
    public LinkedList<String> BinaryList
    {
        get
        {
            return binaryList;
        }
        set
        {
            binaryList = value;
        }
    }
    public LinkedList<Byte> Bytes
    {
        get
        {
            return bytes;
        }
        set
        {
            bytes = value;
        }
    }
    public int ByteOffset
    {
        get
        {
            return byteOffset;
        }
        set
        {
            byteOffset = value;
        }
    }
    public Image Image
    {
        get
        {
            return image;
        }
        set
        {
            image = value;
        }
    }
    public String ImageType
    {
        get
        {
            return imageType;
        }
        set
        {
            imageType = value;
        }
    }
    public int PixelsCount
    {
        get
        {
            return numberOfPixels;
        }
        set
        {
            numberOfPixels = value;
        }
    }

    // private variables
    private LinkedList<String> binaryList;
    private LinkedList<Byte> bytes;
    private int byteOffset;
    private Image image;
    private String imageType;
    private int numberOfPixels;

    public ImageEncoder(Image imageParameter, String imageTypeParameter)
	{
        ImageEncoder.intializeOffsetList(); // initializes the offset list for image encoder

        image = imageParameter;
        imageType = imageTypeParameter;

        if (imageOffset.ContainsKey(imageType)) // make sure it's an acceptable type
        {
            bytes = DataManipulation.imageToByteList(image, imageType);
            binaryList = DataManipulation.convertByteListToBinaryList(bytes, byteSize);
            imageOffset.TryGetValue(imageType, out byteOffset); // will assign byteOffset to the byteOffset parameter
            numberOfPixels = (bytes.Count - byteOffset) / 3;  // each pixel is three bytes
        } else
        {
            throw new Exception("The image type of " + imageType + " is not an acceptable type.");
        }

	}

    // Static methods

    public static void intializeOffsetList ()
    {
        // acceptable types
        if (!imageOffset.ContainsKey("bmp"))
        {
            imageOffset.Add("bmp", 54); // the number of bytes to offset
        }
    }

    // Encryption/Decryption methods

   
   /// <summary>
   /// 
   /// </summary>
   /// <param name="encryptKey"></param>
   /// <param name="randomSeed"></param>
   /// <param name="messageLength">-1 if the message length is not known</param>
   /// <returns></returns>
    public String decrypt (String encryptKey, String randomSeed, int messageLength)
    {
        String[] bytesArray = DataManipulation.linkedListToArray(binaryList); // more efficient for what is being done (accessing random elements inside the array)
        Random positionGenerator = getSeededRandom(randomSeed);
        Dictionary<int, Boolean> pixelsVisited = new Dictionary<int, bool>();

        String message = "";
        Boolean decodingErrorOccured = false;
        // System.Diagnostics.Debug.WriteLine("DECODING");
        String binaryString = "";
        while ((messageLength == -1 || message.Length < messageLength) && pixelsVisited.Count < numberOfPixels && !decodingErrorOccured)
        {
            LinkedList<char> encryptedBits = new LinkedList<char>();
            int bitCounter = 0;
            while (bitCounter < byteSize) // get the byte size number of bits
            {
                int byteInPixelIndex = getUnvisitedByte(positionGenerator, pixelsVisited, colorOffset);
                String byteInPixel = bytesArray[byteInPixelIndex];
                char bit = DataManipulation.getValueInBinaryByte(byteInPixel, byteSize - 1);
                encryptedBits.AddLast(bit); // the message is always embedded in the lsb
                binaryString += bit;
                bitCounter++;
            }
            EncryptString letterEncrypt = new EncryptString(encryptedBits, encryptKey, message.Length); // byteOffset = each letter represents one byte. when doing decryption, the position of the letter in the message matters, since this decrypts part by part and it was encrypted as a whole, an offset is necessary

            // keep trying to decrypt letter by letter because the message length is not known. stop decryption if error is thrown from byte to letter conversion (probably end of message)
            String letter = letterEncrypt.recreateStringFromBytes(); // could throw error because bytes might just be image bytes (message length is not known)
            char letterChar = letter.ToCharArray()[0];
            if (Char.IsLetterOrDigit(letterChar) || Char.IsPunctuation(letterChar) || Char.IsWhiteSpace(letterChar)) 
            {
                message += letter;
            } else // an unkonwn character was returned (AKA not a letter or number or punctuation)
            {
                decodingErrorOccured = true; 
            }
          
        }
        // System.Diagnostics.Debug.WriteLine(binaryString);

        return message;

    }

    /// <summary>
    /// embeds bits into LSB of image 
    /// </summary>
    /// <param name="bitsToEmbed">an array where each element is a bit to embed</param>
    /// <param name="colorOffSet">colorOffset should be between 0 and 2</param>
    public void embedBits (LinkedList<char> bitsToEmbed, String randomSeed, int colorOffSet) 
    {
        String[] bytesArray = DataManipulation.linkedListToArray(binaryList); // converts linked list to array, which is better structure for this task. Maybe should have used arrays throughout
        Random positionGenerator = ImageEncoder.getSeededRandom(randomSeed);
        Dictionary<int, Boolean> pixelsVisited = new Dictionary<int, Boolean>();
        // System.Diagnostics.Debug.WriteLine("EMBEDDING");
        String binaryString = "";
        foreach (char bitToEmbed in bitsToEmbed)
        {
            binaryString += bitToEmbed;

            int byteIndex = getUnvisitedByte(positionGenerator, pixelsVisited, colorOffSet);
            String byteInBinary = bytesArray[byteIndex];
            byteInBinary = DataManipulation.modifyBinaryByte(byteInBinary, byteSize - 1, bitToEmbed); // modify the least significant bit

            bytesArray[byteIndex] = byteInBinary; // Update the byte with the new modified byte
        }
        binaryList = new LinkedList<string>(bytesArray); // convert the bytes array back into a linkedlist
        recreateDataStructuresFromBits(); // update the linkedlist with actual byte objects
        recreateImageFromBytes(); // update the actual image

        // System.Diagnostics.Debug.WriteLine(binaryString);
    }

    public void embedMessage(String message, String encryptKey, String randomSeed)
    {
        EncryptString encryptString = new EncryptString(message);
        encryptString.encrypt(encryptKey);
        embedBits(encryptString.BitList, randomSeed, colorOffset); // embed the bits

        // All data structures will be updated. manually resave the image
    }

    
    // Data manipulation methods


    /// <summary>
    /// Assumes bitList has been changed in some way, so recreate everything else, EXCEPT for the Image from the singleBits
    /// </summary>
    public void recreateDataStructuresFromBits()
    {
        bytes = DataManipulation.convertBinaryListToByteList(BinaryList, byteSize);
    }

    /// <summary>
    /// Recreates the Image from bytes. Will assign the image private variable to the output
    /// </summary>
    /// <returns></returns>
    public Image recreateImageFromBytes()
    {
        image = DataManipulation.imageFromByteList(bytes);
        return image;
    }

    // MISC METHODS 


    /// <summary>
    /// 
    /// </summary>
    /// <param name="positionGenerator">the seeded position generator</param>
    /// <param name="pixelsVisited">a dictionary of pixels that have been visited; key is the pixel index</param>
    /// <param name="colorOffSet">the color offset (should be between 0-2) where 0 represents red</param>
    /// <returns>the byte index of an unvisited byte</returns>
    public int getUnvisitedByte(Random positionGenerator, Dictionary<int, Boolean> pixelsVisited, int colorOffSet)
    {
        int byteIndex = -1;
        while (byteIndex == -1)
        { // get a unique, random position to embed the bit in
            int pixel = positionGenerator.Next(0, numberOfPixels); // pick a pixel
            Boolean visited;
            pixelsVisited.TryGetValue(pixel, out visited);
            if (!visited)
            {
                pixelsVisited.Add(pixel, true);
                byteIndex = pixel * 3 + colorOffSet; // each pixel has 3 bytes
            }
        }
        return byteIndex;
    }


    /// <summary>
    /// Saves image
    /// Will OVERRIDE existing image if one has the same path
    /// </summary>
    /// <param name="filePath">Should include fileName and extension (extension should match file type)</param>
    public void saveImageToFile (String filePath)
   {
        FileSystemInteraction.deleteFile(filePath);
        image.Save(filePath);
   }

    // STATIC METHODS


    public static Random getSeededRandom(String seedString)
    {
        int seed = 0;
        int mathOperator = 1;
        foreach (char letter in seedString) // convert each char int an int representation and add or subtract it to generate a (relatively) unique seed
        {
            byte letterByte = Convert.ToByte(letter);
            seed += letterByte * mathOperator; // mathOperator makes order count more by adding or subtracting (alternating between the two)
            if (mathOperator == 1)
            {
                mathOperator = -1;
            } else
            {
                mathOperator = 1;
            }
        }

        return new Random(seed); // used to determine the random positions
    }




}

