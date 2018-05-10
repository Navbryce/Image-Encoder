using System;
using System.Collections.Generic;
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

        public static LinkedList<char> bitXORListReverse (LinkedList<char> xorBits, LinkedList<char> originalCompareBits)
        {
            LinkedList<char> originalBits = new LinkedList<char>();

            LinkedListNode<char> mainNode = xorBits.First;
            LinkedListNode<char> compareNode = originalCompareBits.First;

            while (mainNode != null)
            {
                char output = bitXORReverse(mainNode.Value, compareNode.Value);
                originalBits.AddLast(output);

                mainNode = mainNode.Next;
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

    }
}
