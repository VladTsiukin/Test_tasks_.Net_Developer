using System;
using System.Diagnostics;


namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Universe universe = new Universe(37, 37, 7);
                universe.StartLife();
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Debug.Fail(e.Message);
                ShowError();
            }
            catch (Exception e)
            {
                //Debug.Fail(e.Message);
                ShowError();
            }
        }

        private static void ShowError()
        {
            Console.WriteLine("An error occurred. Please, try restart the game.");
        }
    }
}