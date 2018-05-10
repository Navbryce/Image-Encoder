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
    public LinkedList<char> BitList
    {
        get
        {
            return BitList;
        }
        set
        {
            BitList = value;
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

    // private variables
    private LinkedList<String> binaryList;
    private LinkedList<char> bitList;
    private LinkedList<Byte> bytes;
    private Image image;
    private String imageType;

    public ImageEncoder(Image imageParameter, String imageTypeParameter)
	{
        ImageEncoder.intializeOffsetList(); // initializes the offset list for image encoder

        image = imageParameter;
        imageType = imageTypeParameter;

        if (imageOffset.ContainsKey(imageType)) // make sure it's an acceptable type
        {
            bytes = DataManipulation.imageToByteList(image, imageType);
            binaryList = DataManipulation.convertByteListToBinaryList(bytes, byteSize);
            bitList = DataManipulation.convertBinaryListToBitList(binaryList);

        } else
        {
            throw new Exception("The image type of " + imageType + " is not an acceptable type.");
        }

	}

    public static void intializeOffsetList ()
    {
        // acceptable types
        imageOffset.Add("bmp", 54); // the number of bytes to offset
    }


    /// <summary>
    /// Data manipulation methods
    /// </summary>
    /*
     * Assumes bitList has been changed in some way, so recreate everything else, EXCEPT for the Image from the singleBits
     * */
    public void recreateDataStructuresFromBits()
    {
        binaryList = DataManipulation.convertBitListToBinaryList(bitList, byteSize);
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


}

