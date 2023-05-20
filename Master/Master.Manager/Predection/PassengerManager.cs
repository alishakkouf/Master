﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronXL;
using Master.Domain.Passenger;
using Master.Domain.Trips;
using Master.Shared;
using Microsoft.Extensions.Localization;

namespace Master.Manager.Predection
{
    public class PassengerManager : IPassengerManager
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer _localizer;

        public PassengerManager( ICurrentUserService currentUserService,
            IStringLocalizerFactory factory)
        {
            _currentUserService = currentUserService;
            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task<PassengerOpinionDomain> GetPassengerOpinion(GetPassengerOpinionDomain input)
        {
            int numberOfProperties = 23;  // number predictors (gender, age, class..)
            int numberOfClases = 2;  // number classes (satisfied and dissatisfied)

            // Supported spreadsheet formats for reading include: XLSX, XLS, CSV and TSV
            WorkBook workbook = WorkBook.Load("D:\\Master\\Master\\Master.Manager\\DataTest\\z.xlsx");

            //to read specific sheet
            var worksheet = workbook.WorkSheets.First();

            int datasetSize = worksheet.Rows.Count(); /* number data items = //*/ /*39;*/

            string[][] data = new string[datasetSize][]; // get from csv

            var ignorFirstRow = true;

            var index = 0;
            foreach (var row in worksheet.Rows)
            {
                if (ignorFirstRow)
                {
                    ignorFirstRow = false;
                    continue;
                }
                //  m[0] = new string[] { "actor", "green", "korea", "1" };
               
                data[index] = new string[] { };

                var xxxx = new string[23];
                for (int i = 0; i < numberOfProperties; i++)
                {
                    xxxx[i] = row.Columns[i].ToString();
                   
                }

                data[index] = xxxx;
               index++;
            }



            int[][] jointCounts = new int[numberOfProperties][];
            for (int i = 0; i < numberOfProperties; ++i)
                jointCounts[i] = new int[numberOfClases];

            int[] yCounts = new int[numberOfClases];

            string[] X = new string[] 
            { 
                input.Gender,
                input.CustomerType,
                input.AgeClass,
                input.TypeOfTravel,
                input.Class,
                input.FlightDistanceClass,
                input.InflightWifiService + "",
                input.DepartureArrivalTimeConvenient + "",
                input.EaseOfOnlineBooking + "",
                input.Gender,
                input.GateLocation + "",
                input.FoodAndDrink + "",
                input.OnlineBoarding + "",
                input.SeatComfort + "",
                input.InflightEntertainment + "",
                input.OnBoardService + "",
                input.LegRoomService + "",
                input.BaggageHandling + "",
                input.CheckinService + "",
                input.InflightService + "",
                input.Cleanliness + "",
                input.DepartureDelayInMinutesClass,
                input.ArrivalDelayInMinutesClass
            }; //represents the passenger inputs
         

            Console.WriteLine("\nItem to classify: ");
            Console.WriteLine(X);

            for (int i = 0; i < datasetSize; ++i)
            {   // compute joint counts
                int y = int.Parse(data[i][numberOfProperties-1]);  // get the class as int
                ++yCounts[y];

                Array.Sort(yCounts);

                for (int j = 0; j < numberOfProperties; ++j)
                {
                    if (data[i][j] == X[j])
                        ++jointCounts[j][y];
                }
            }

            for (int i = 0; i < numberOfProperties; ++i)  // Laplacian smoothing
                for (int j = 0; j < numberOfClases; ++j)
                    ++jointCounts[i][j];

            Console.WriteLine("\nJoint counts (smoothed): ");
            Console.WriteLine("0 1 2 ");
            Console.WriteLine("------");
            ShowMatrix(jointCounts);

            Console.WriteLine("\nClass counts (raw): ");
            ShowVector(yCounts);

            // compute evidence terms
            double[] eTerms = new double[numberOfClases];
            for (int k = 0; k < numberOfClases; ++k)
            {
                double v = 1.0;  // direct approach
                for (int j = 0; j < numberOfProperties; ++j)
                    v *= (jointCounts[j][k] * 1.0) / (yCounts[k] + numberOfProperties);
                v *= (yCounts[k] * 1.0) /datasetSize;
                eTerms[k] = v;

            }

            Console.WriteLine("\nEvidence terms for each class: ");
            ShowVector(eTerms);

            double evidence = 0.0;
            for (int k = 0; k < numberOfClases; ++k)
                evidence += eTerms[k];

            double[] probs = new double[numberOfClases];
            for (int k = 0; k < numberOfClases; ++k)
                probs[k] = eTerms[k] / evidence;

            Console.WriteLine("\nPseudo-probabilities each class: ");
            ShowVector(probs);

            Console.WriteLine("\nEnd naive Bayes demo ");
            Console.ReadLine();

            
            return new PassengerOpinionDomain
            {
                Satisfied = probs[0],
                Disatisfied = probs[1]
            };
        } // Main

        static void ShowMatrix(int[][] m)
        {
            int r = m.Length; int c = m[0].Length;
            for (int i = 0; i < r; ++i)
            {
                for (int j = 0; j < c; ++j)
                {
                    Console.Write(m[i][j] + " ");
                }
                Console.WriteLine("");
            }
        }

        static void ShowVector(int[] v)
        {
            for (int i = 0; i < v.Length; ++i)
                Console.Write(v[i] + "  ");
            Console.WriteLine("");
        }

        static void ShowVector(double[] v)
        {
            for (int i = 0; i < v.Length; ++i)
                Console.Write(v[i].ToString("F4") + "  ");
            Console.WriteLine("");
        }

               
    }
}
