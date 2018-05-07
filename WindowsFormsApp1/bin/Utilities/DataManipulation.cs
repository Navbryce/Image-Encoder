using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.bin.Utilities
{
    class DataManipulation
    {
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
