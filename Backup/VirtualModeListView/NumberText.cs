using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualModeListView
{
    public class NumberText
    {
        private const int milliarde = 1000000000;
        private const int million = 1000000;
        private const int tausend = 1000;

        private string[] m_BasicNumberText = new string[13];
        private Dictionary<int, string> m_OneDigitDec10Text = new Dictionary<int, string>();
        private string[] m_DecText = new string[10];
        
        public NumberText()
        {           
            m_BasicNumberText[0] = "null";
            m_BasicNumberText[1] = "eins";
            m_BasicNumberText[2] = "zwei";
            m_BasicNumberText[3] = "drei";
            m_BasicNumberText[4] = "vier";
            m_BasicNumberText[5] = "fünf";
            m_BasicNumberText[6] = "sechs";
            m_BasicNumberText[7] = "sieben";
            m_BasicNumberText[8] = "acht";
            m_BasicNumberText[9] = "neun";
            m_BasicNumberText[10] = "zehn";
            m_BasicNumberText[11] = "elf";
            m_BasicNumberText[12] = "zwölf";

            m_OneDigitDec10Text.Add(6, "sech");
            m_OneDigitDec10Text.Add(7, "sieb");
            
            m_DecText[0] = "";
            m_DecText[1] = "zehn";
            m_DecText[2] = "zwanzig";
            m_DecText[3] = "dreissig";
            m_DecText[4] = "vierzig";
            m_DecText[5] = "fünfzig";
            m_DecText[6] = "sechzig";
            m_DecText[7] = "siebzig";
            m_DecText[8] = "achtzig";
            m_DecText[9] = "neunzig";

        }

        private string ScanNumber(string digits)
        {
            string numb = string.Empty;
            int count = digits.Length;

            switch (count)
            {
                case 1:
                    numb = MakeTextOneDigit(digits);
                    break;
                case 2:
                    numb = MakeTextTwoDigits(digits);
                    break;
                case 3:
                    numb = MakeTextThreeDigits(digits);
                    break;
            }

            return numb;
        }       

        private string MakeTextOneDigit(string digit)
        {
            int chr = digit[0];
            int inx = chr - 48;
            return m_BasicNumberText[inx];
        }

        private string MakeTextTwoDigits(string digits)
        {
            int chr1 = digits[0];
            int inx1 = chr1 - 48;

            int chr2 = digits[1];
            int inx2 = chr2 - 48;

            if (digits.Equals("10")) return m_BasicNumberText[10];
            if (digits.Equals("11")) return m_BasicNumberText[11];
            if (digits.Equals("12")) return m_BasicNumberText[12];

            if (inx1 > 1)
            {
                if (inx2 == 0)
                {                   
                    string oops = m_DecText[inx1];
                    return oops;
                }
                else if (inx2 == 1)
                {
                    string oops = m_DecText[inx1];
                    return "einund" + oops;
                }
                else
                {
                    string achso = GetDecText(inx1, inx2);
                    string oops = m_DecText[inx1];

                    return achso + "und" + oops;
                }
            }
            else
            {
                string achso = GetDecText(inx1, inx2);
                string oops = m_DecText[inx1];

                return achso + oops;
            }
        }

        private string GetDecText(int inx1, int inx2)
        {
            string txt = string.Empty;

            txt = m_BasicNumberText[inx2];

            if (inx1 == 1)
            {
                if (m_OneDigitDec10Text.ContainsKey(inx2))
                {
                    txt = m_OneDigitDec10Text[inx2];
                }
            }

            return txt;
        }

        private string MakeTextThreeDigits(string digits)
        {
            int chr1 = (int)digits[0];
            int inx1 = chr1 - 48;

            string oops = string.Empty;
            string wow = string.Empty;

            if (inx1 > 1)
            {
                oops = m_BasicNumberText[inx1];
            }
            else
            {
                oops = "ein";
            }
            
            string txt100 = "hundert";

            string cool = digits.Substring(1, 2);

            if (!cool.Equals("00"))
            {
                wow = MakeTextTwoDigits(cool);
            }

            return oops + txt100 + wow;
        }


        
        public string MakeText(int no)
        {
            string numtxt = string.Empty;
            
            int mia = no / milliarde;
            int rest = no % milliarde;

            int mio = rest / million;
            rest = rest % million;

            int tau = rest / tausend;
            rest = rest % tausend;

            if (mia > 0)
            {
                if (1 == mia)
                {
                    numtxt = "einemilliarde";
                }
                else
                {
                    string miatxt = ScanNumber(mia.ToString());
                    numtxt = miatxt += "milliarden";
                }
            }

            if (mio > 0)
            {
                if (1 == mio)
                {
                    numtxt = "einemillion";
                }
                else
                {
                    string miotxt = ScanNumber(mio.ToString());
                    numtxt += miotxt += "millionen";
                }
            }

            if (tau > 0)
            {
                if (1 == tau)
                {
                    numtxt = "eintausend";
                }
                else
                {
                    string tautxt = ScanNumber(tau.ToString());
                    numtxt += tautxt + "tausend";
                }
            }
                        
            if (rest > 0)
            {
                string resttxt = ScanNumber(rest.ToString());
                numtxt += resttxt;
            }

            if (no > 0)
            {
                return numtxt;
            }
            else
            {
                return m_BasicNumberText[0];
            }
        }

    }
}
