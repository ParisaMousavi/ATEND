using System;
using System.Collections.Generic;
using System.Text;

namespace Atend.Global.Calculation.Electrical
{

    public class Complex : System.ICloneable
    {

        ///<summary>
        ///قسمت حقیقی عدد مختلط
        ///</summary>
        public double real;
        ///<summary>
        ///قسمت موهومی عدد مختلط
        ///</summary>
        public double imag;

        ///<summary>
        ///ایجاد یک عدد مختلط با دریافت قسمتهای حقیقی و موهومی
        ///</summary>
        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imag = imaginary;
        }

        ///<summary>
        ///ایجاد عدد مختلط از یک عدد حقیقی و قرار دادن قسمت موهومی برابر صفر
        ///</summary>
        public Complex(double real)
        {
            this.real = real;
            this.imag = 0;
        }

        ///<summary>
        ///ایجاد عدد مختلط با دریافت مختصات قطبی یا کارتزین
        ///</summary>
        public Complex(double real, double imaginary, bool polar)
        {

            if (polar)
            {
                this.FromPolar(real, imaginary);
            }
            else
            {
                this.real = real;
                this.imag = imaginary;
            }
        }

        ///<summary>
        ///ایجاد یک عدد مختلط برابر صفر
        ///</summary>
        public Complex()
        {
            this.real = 0;
            this.imag = 0;
        }

        ///<summary>
        ///اندازه عدد مختلط در مختصات قطبی
        ///</summary>
        public double abs
        {
            get { return Math.Sqrt(this.real * this.real + this.imag * this.imag); }
        }

        ///<summary>
        ///زاویه عدد مختلط در مختصات قطبی
        ///</summary>
        public double arg
        {
            get
            {
                if (this.real > 0)
                    return Math.Atan(this.imag / this.real);
                else if (this.real < 0)
                    return Math.PI + Math.Atan(this.imag / this.real);
                else
                    if (this.imag > 0) return Math.PI / 2;
                    else if (this.imag < 0) return -Math.PI / 2;
                    else return 0;
            }
        }

        ///<summary>
        ///تبدیل از مختصات قطبی به کارتزین
        ///</summary>
        public void FromPolar(double modulus, double arg)
        {
            this.real = modulus * Math.Cos(arg);
            this.imag = modulus * Math.Sin(arg);
        }

        ///<summary>
        ///جمع دو عدد مختلط
        ///</summary>
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.real + c2.real, c1.imag + c2.imag);
        }

        ///<summary>
        ///تفریق دو عدد مختلط
        ///</summary>
        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.real - c2.real, c1.imag - c2.imag);
        }

        ///<summary>
        ///ضرب دو عدد مختلط
        ///</summary>
        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.real * c2.real - c1.imag * c2.imag,
                c1.real * c2.imag + c1.imag * c2.real);
        }

        ///<summary>
        ///تقسیم دو عدد مختلط
        ///</summary>
        public static Complex operator /(Complex c1, Complex c2)
        {
            return new Complex(c1.abs / c2.abs, c1.arg - c2.arg, true);
        }

        ///<summary>
        ///تساوی دو عدد مختلط
        ///</summary>
        public static bool operator ==(Complex c1, Complex c2)
        {
            return (c1.real == c2.real) && (c1.imag == c2.imag);
        }

        ///<summary>
        ///عدم تساوی دو عدد مختلط
        ///</summary>
        public static bool operator !=(Complex c1, Complex c2)
        {
            return (c1.real != c2.real) || (c1.imag != c2.imag);
        }

        ///<summary>
        ///منفی کردن عدد مختلط
        ///</summary>
        public static Complex operator -(Complex c1)
        {
            return new Complex(-c1.real, c1.imag);
        }

        ///<summary>
        ///ضرب یک عدد حقیقی در یک عدد مختلط
        ///</summary>
        ///همچنین برای انجام سایر عملیات می توان ابتدا عدد حقیقی را به مختلط تبدیل
        ///و سپس عملیات لازم را انجام داد
        ///به عنوان مثال:
        ///Complex c = new Complex(2, 3) / new Complex(5);
        public static Complex operator *(double r, Complex c)
        {
            return new Complex(c.real * r, c.imag * r);
        }

        ///<summary>
        ///مزدوج مختلط
        ///</summary>
        public static Complex conj(Complex c1)
        {
            return new Complex(c1.real, -c1.imag);
        }

        public override string ToString()
        {
            bool b = new Complex() == new Complex();
            return (System.String.Format("{0} + {1}i", real, imag));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public override bool Equals(object o)
        {
            return (Complex)o == this;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}


