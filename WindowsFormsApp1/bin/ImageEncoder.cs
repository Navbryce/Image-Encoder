using System;
using System.Collections.Generic;
using System.Drawing;
using WindowsFormsApp1.bin.Utilities;

public class ImageEncoder
{
    // Static variables
    public static int byteSize = 8;
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
        imageOffset.Add("bmp", 54); // the number of bytes to offset
    }

    // Encryption/Decryption methods

    /// <summary>
    /// embeds bits into LSB of image 
    /// </summary>
    /// <param name="bitsToEmbed">an array where each element is a bit to embed</param>
    /// <param name="colorOffSet">colorOffset should be between 0 and 2</param>
    public void embedBits (LinkedList<char> bitsToEmbed, String randomSeed, int colorOffSet) 
    {
        String[] bytesArray = DataManipulation.linkedListToArray(binaryList); // converts linked list to array, which is better structure for this task. Maybe should have used arrays throughout
        Random positionGenerator = ImageEncoder.getSeededRandom(randomSeed);
        Dictionary<int, String> bytesVisited = new Dictionary<int, String>();

        foreach (char bitToEmbed in bitsToEmbed)
        {
            int byteIndex = -1;
            while (byteIndex == -1) { // get a unique, random position to embed the bit in
                
            }
        }
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

    public static Random getSeededRandom(String seedString)
    {
        int seed = Int32.Parse(seedString); // Converts the seed to an int. must be done to seed the random generator
        return new Random(seed); // used to determine the random positions
    }

    /// <summary>
    /// Saves image
    /// </summary>
    /// <param name="filePath">Should include fileName and extension (extension should match file type)</param>
    public void saveImageToFile (String filePath)
   {
        image.Save(filePath);
   }


}

