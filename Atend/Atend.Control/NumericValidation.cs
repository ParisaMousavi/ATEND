using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Atend.Control
{
    public class NumericValidation
    {
        public static bool DoubleConverter(string Number)
        {
            try
            {
                Convert.ToDouble(Number);
                return true;
            }
            catch 
            {

                return false;
            }
        }

        public static bool Int32Converter(string Number)
        {
            try
            {
                Convert.ToInt32(Number);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public static bool Int64Converter(string Number)
        {
            try
            {
                Convert.ToInt64(Number);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public static bool Int16Converter(string Number)
        {
            try
            {
                Convert.ToInt16(Number);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public static bool DateConverter(string Date)
        {
       
            try
            {
                Convert.ToDateTime(Date);
                return true;
            }
            catch
            {

                return false;
            }
       
        }

        public static byte[] StructureToByteArray(object obj)
        {
            int Length = Marshal.SizeOf(obj);
            byte[] bytearray = new byte[Length];
            IntPtr ptr = Marshal.AllocHGlobal(Length);
            Marshal.StructureToPtr(obj, ptr, false);
            Marshal.Copy(ptr, bytearray, 0, Length);
            Marshal.FreeHGlobal(ptr);
            return bytearray;
        }

        public static void ByteArrayToStructure(byte[] bytearray, ref object obj)
        {
            int Length = Marshal.SizeOf(obj);
            IntPtr ptr = Marshal.AllocHGlobal(Length);
            Marshal.Copy(bytearray, 0, ptr, Length);
            obj = Marshal.PtrToStructure(ptr, obj.GetType());
            Marshal.FreeHGlobal(ptr);
        }


    }
}
