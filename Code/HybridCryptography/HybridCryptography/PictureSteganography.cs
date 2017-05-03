using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HybridCryptography
{

        public class PictureSteganographyHelper
        {
            public int TextBitsIndex { get; set; }
            public string TextBits { get; set; }
            public bool TextIsBeingProcessed { get; set; }
            public const int LENGTH_BITS_COUNT = 18;        //Het aantal bits gereserveerd voor de lengte van de tekst

            public Bitmap embedText(string text, Bitmap image)
            {
                TextBitsIndex = 0;
                TextBits = ConvertStringToBits(text);
                TextIsBeingProcessed = true;
                try
                {
                    embedTextLength(text, image);
                    for (int i = 0; i < image.Width; i++)
                    {
                        for (int j = (LENGTH_BITS_COUNT / 3) + 1; j < image.Height; j++)
                        {
                            if (TextBitsIndex < TextBits.Length)
                            {
                                Color pixel = ClearLeastSignificantBit(image.GetPixel(i, j));
                                pixel = EmbedTextBitsInPixel(pixel);
                                image.SetPixel(i, j, pixel);
                            }
                            else
                            {
                                if (TextIsBeingProcessed)
                                {
                                    Color pixel = ClearLeastSignificantBit(image.GetPixel(i, j));
                                    image.SetPixel(i, j, pixel);
                                    TextIsBeingProcessed = false;
                                }
                            }
                        }
                    }
                    return image;
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException("Failed: No image selected");
                }

                return null;
            }

            private void embedTextLength(string text, Bitmap image)   //Plaats de lengte van de tekst in de eerste 18 bits
            {                                                       //Max lengte = 2^18 => 262 144 bits = 32 768 tekens
                string bits = Convert.ToString(text.Length, 2).PadLeft(LENGTH_BITS_COUNT, '0');
                Color pixel;
                for (int i = 0; i < LENGTH_BITS_COUNT / 3; i++)
                {
                    pixel = image.GetPixel(0, i);
                    pixel = ClearLeastSignificantBit(pixel);
                    int R = pixel.R + Convert.ToInt32(bits[i * 3].ToString());
                    int G = pixel.G + Convert.ToInt32(bits[i * 3 + 1].ToString());
                    int B = pixel.B + Convert.ToInt32(bits[i * 3 + 2].ToString());
                    image.SetPixel(0, i, Color.FromArgb(R, G, B));
                }
            }

            private int extractTextLength(Bitmap image)
            {
                Color pixel;
                StringBuilder bits = new StringBuilder();
                for (int i = 0; i < LENGTH_BITS_COUNT / 3; i++)
                {
                    pixel = image.GetPixel(0, i);
                    bits.Append(pixel.R % 2);
                    bits.Append(pixel.G % 2);
                    bits.Append(pixel.B % 2);
                }
                return Convert.ToInt32(bits.ToString(), 2);
            }

            public string extractText(Bitmap image)
            {
                Color pixel;
                int textLength = extractTextLength(image) * 8;
                double textLengthInPixels = Math.Ceiling((double)textLength / 3);
                TextBitsIndex = 0;
                StringBuilder textBits = new StringBuilder();
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = (LENGTH_BITS_COUNT / 3) + 1; j < image.Height && TextBitsIndex < textLengthInPixels; j++)
                    {
                        pixel = image.GetPixel(i, j);
                        textBits.Append(pixel.R % 2);
                        textBits.Append(pixel.G % 2);
                        textBits.Append(pixel.B % 2);
                        TextBitsIndex++;
                    }
                }
                return ConvertBitsToString(textBits.ToString());
            }

            private Color EmbedTextBitsInPixel(Color pixel)
            {
                int R = pixel.R + GetNextBitFromTextBits();
                int G = pixel.G + GetNextBitFromTextBits();
                int B = pixel.B + GetNextBitFromTextBits();
                Color pixelToReturn = Color.FromArgb(R, G, B);
                return pixelToReturn;
            }

            private Color ClearLeastSignificantBit(Color pixel)
            {
                int R = pixel.R - pixel.R % 2;
                int G = pixel.G - pixel.G % 2;
                int B = pixel.B - pixel.B % 2;
                Color pixelToReturn = Color.FromArgb(R, G, B);
                return pixelToReturn;
            }

            private string ConvertStringToBits(string text)
            {
                byte[] bytes = ASCIIEncoding.Default.GetBytes(text);
                StringBuilder bits = new StringBuilder();
                string byteInBits;
                for (int i = 0; i < bytes.Length; i++)
                {

                    byteInBits = Convert.ToString(bytes[i], 2); // Convert from Byte to Bin
                    byteInBits = byteInBits.PadLeft(8, '0');  // Zero Pad

                    bits.Append(byteInBits);
                }
                return bits.ToString();
            }

            private string ConvertBitsToString(string bits)
            {
                byte characterBytes;
                StringBuilder text = new StringBuilder();
                for (int i = 0; i < bits.Length / 8; i++)
                {
                    string temp = bits.Substring(i * 8, 8);
                    characterBytes = Convert.ToByte(temp, 2);
                    text.Append(Convert.ToChar(characterBytes));
                }
                return text.ToString();
            }

            private int GetNextBitFromTextBits()
            {
                if (TextBitsIndex < TextBits.Length)
                {
                    int bit = Convert.ToInt32(TextBits[TextBitsIndex].ToString());
                    TextBitsIndex++;
                    return bit;
                }
                return 0;
            }
        }
}
