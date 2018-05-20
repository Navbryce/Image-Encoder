using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.bin.Utilities
{
    class DataManipulation
    {
        // Encryption Functions
        public static char bitXOR (char bit_one, char bit_two)
        {
            char outputBit;
            if (bit_one != bit_two) // one of the bits must be true (both can't be true or false)
            {
                outputBit = '1';
            } else
            {
                outputBit = '0';
            }
            return outputBit;
        }

        public static char bitXORReverse (char xorBit, char originalCompareBit)
        {
            char originalBit;
            if (xorBit == '1')
            {
                if (originalCompareBit == '1')
                {
                    originalBit = '0';
                } else
                {
                    originalBit = '1';
                }
            } else
            {
                originalBit = originalCompareBit;
            }
            return originalBit;
        }

        /**
         * Will run bitXOR on each mainBit and its corresponding compareBits. If compareBits is smaller than mainBits, it will just loop for the compareBits
         * */
        public static LinkedList<char> bitXORList (LinkedList<char> mainBits, LinkedList<char> compareBits)
        {
            LinkedList<char> outputList = new LinkedList<char>();

            LinkedListNode<char> mainNode = mainBits.First;
            LinkedListNode<char> compareNode = compareBits.First;

            while (mainNode != null)
            {
                char output = bitXOR(mainNode.Value, compareNode.Value);
                outputList.AddLast(output);

                mainNode = mainNode.Next;
                compareNode = compareNode.Next;
                if (compareNode == null) // all of the compare bits have been iterated through, so restart from the beginning
                {
                    compareNode = compareBits.First;
                }
            }
            return outputList;

        }

        public static LinkedList<char> bitXORListReverse (LinkedList<char> xorBits, LinkedList<char> originalCompareBits, int compareBitOffset)
        {
            LinkedList<char> originalBits = new LinkedList<char>();

            LinkedListNode<char> mainNode = xorBits.First;
            LinkedListNode<char> compareNode = originalCompareBits.First;

           
            while (mainNode != null)
            {
                if (compareBitOffset == 0) // sometimes the compare bits need to be offset (you need to jump forward)
                {
                    char output = bitXORReverse(mainNode.Value, compareNode.Value);
                    originalBits.AddLast(output);

                    mainNode = mainNode.Next;
                } else
                {
                    compareBitOffset -= 1; // the code outside the else (right below) pushes the compare node one bit forward
                }
                compareNode = compareNode.Next;
                if (compareNode == null) // all of the compare bits have been iterated through, so restart from the beginning
                {
                    compareNode = originalCompareBits.First;
                }
            }
            return originalBits;
        }

        // Small data object functions
        
         /**
         * Assumes the user wants to make an 8-bit byte
         * 
         * */
        public static Byte convertBinaryToByte (String binaryValue)
        {
            return Convert.ToByte(binaryValue, 2); 
        }
        public static String convertByteToBinary (Byte byteValue, int byteSize)
        {
            return Convert.ToString(byteValue, 2).PadLeft(byteSize, '0');
        }
        public static char getValueInBinaryByte (String byteInBinary, int bitIndex)
        {
            char resultChar;
            if (bitIndex >= byteInBinary.Length)
            {
                throw new Exception("You are trying to view a byte at an index (" + bitIndex + ") that is bigger than the actual byte.");
            }
            else
            {
                resultChar = byteInBinary.ToCharArray()[bitIndex];
            }
            return resultChar;
        }
        public static String modifyBinaryByte (String byteInBinary, int bitIndex, char newBitValue)
        {
            String resultByte = "";
            if (bitIndex >= byteInBinary.Length)
            {
                throw new Exception("You are trying to modify a byte at an index (" + bitIndex + ") that is bigger than the actual byte.");
            } else
            {
                resultByte = byteInBinary.Substring(0, bitIndex) + newBitValue;
                if (bitIndex < byteInBinary.Length - 1) // get the rest of the byte if the bit that is being modified is not at the end of the byte
                {
                    resultByte += byteInBinary.Substring(bitIndex + 1); 
                }
            }
            return resultByte;
        }

        // List functions

        /**
         * ASSUMES binary is 8-bit
         * */
        public static LinkedList<Byte> convertBinaryListToByteList (LinkedList<String> binaryList, int byteSize) {
            LinkedList<Byte> bytes = new LinkedList<byte>();
            foreach (String binaryString in binaryList)
            {
                bytes.AddLast(convertBinaryToByte(binaryString));
            }
            return bytes;
        }
        /**
         * Converts an array of string bytes (in binary) to an array where each element is a single bit
         * */
        public static LinkedList<char> convertBinaryListToBitList (LinkedList<String> binaryList)
        {
            LinkedList<char> stringBits = new LinkedList<char>();
            foreach (String binaryByte in binaryList)
            {
                foreach (char bit in binaryByte)
                {
                    stringBits.AddLast(bit);
                }
            }
            return stringBits;
        }

        /**
         * Converts an array of single bits to an array of string bytes (in binary)
         * */
        public static LinkedList<String> convertBitListToBinaryList (LinkedList<char> bitList, int byteSize)
        {
            LinkedList<String> binaryByteList = new LinkedList<string>();
            if (bitList.Count % byteSize != 0)
            {
                throw new SystemException("Can't produce a whole number of bytes from the inputted bitlist and bytesize");
            } else
            {
                int bitCounter = 0;
                String byteString = "";
                foreach (char bit in bitList)
                {
                    byteString += bit;
                    bitCounter++;
                    if (bitCounter == 8)
                    {
                        binaryByteList.AddLast(byteString);

                        // a complete byte has been added, so start a new byte
                        bitCounter = 0;
                        byteString = "";
                    }
                }
            }
            return binaryByteList;
        }
        public static LinkedList<String> convertByteListToBinaryList (LinkedList<Byte> bytes, int byteSize)
        {
            LinkedList<String> binaryList = new LinkedList<string>();
            foreach (Byte byteObject in bytes)
            {
                binaryList.AddLast(convertByteToBinary(byteObject, byteSize));
            }
            return binaryList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>returns a string (will NEVER throw an error even if the byte represents an unknown character)</returns>
        public static String convertBytesToString (LinkedList<Byte> bytes) 
        {
            return System.Text.Encoding.UTF8.GetString(bytes.ToArray(), 0, bytes.Count);
        }
        public static LinkedList<Byte> convertStringToBytes (String stringValue)
        {
            LinkedList<Byte> bytesList = new LinkedList<byte>();
            Byte[] bytes = Encoding.ASCII.GetBytes(stringValue);
            foreach (Byte byteObject in bytes)
            {
                try
                {
                    bytesList.AddLast(byteObject);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return bytesList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">type of objects in linked list</typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T[] linkedListToArray<T>(LinkedList<T> list)
        {
            T[] array = new T[list.Count];
            list.CopyTo(array, 0); // array more efficient for this. probably should have used arrays throughout
            return array;
        }

        // Image functions
        public static Image imageFromByteList(LinkedList<Byte> byteList)
        {
            Byte[] bytes = byteList.ToArray();
            MemoryStream stream = new MemoryStream(bytes); // write the bytes to the MemoryStream
            return Image.FromStream(stream);
        }
        public static LinkedList<Byte> imageToByteList(Image image, String imageType)
        {
            LinkedList<Byte> resultList;
            MemoryStream stream = new MemoryStream();

            imageType = imageType.ToLower();
            ImageFormat format;
            if (imageType.Equals("bmp"))
            {
                format = System.Drawing.Imaging.ImageFormat.Bmp;
            } else
            {
                throw new Exception("The image of type " + imageType + " could not be converted to a byte array. It is not an acceptable type.");
            }

            image.Save(stream, format); // write the image to a byte a memory stream
            Byte[] byteArray = stream.ToArray(); // convert memory stream to array
            resultList = new LinkedList<byte>(byteArray); // convert array to linked list
            return resultList;
        }


    }
}
