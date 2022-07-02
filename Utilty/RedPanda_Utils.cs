using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedPanda
{
    public static class RedPanda_Utils
    {
        public static int Randomize(int maxValue) => CalculateInt(0, maxValue);
        public static int Randomize(int minValue, int maxValue) => CalculateInt(minValue, maxValue);
        public static float Randomize(float maxValue) => CalculateFloat(0f, maxValue);
        public static float Randomize(float minValue, float maxValue) => CalculateFloat(minValue, maxValue);
        public static T GetRandomValueOfArray<T>(this T[] array) => array[Randomize(array.Length)];
        public static T GetRandomValueOfList<T>(this List<T> list) => list[Randomize(list.Count)];
        public static T GetMaxValueOfList<T>(this List<T> list) => SortListAndGetElement(list, list.Count - 1);
        public static T GetMinValueOfList<T>(this List<T> list) => SortListAndGetElement(list, 0);
        public static void SpawnObjectBetweenTwoPoints(GameObject objectToSpawn, Vector3 firstPoint, Vector3 secondPoint) => objectToSpawn.transform.SetPositionAndRotation((firstPoint + secondPoint) / 2, Quaternion.Euler(new Vector3(0, AngleBetweenTwoVectors(firstPoint, secondPoint), 0)));
        public static void ToDictionary(this Dictionary<string, Queue<GameObject>> dictionary, string tag, GameObject obj) => dictionary[tag].Enqueue(obj);
        public static GameObject FromDictionary(this Dictionary<string, Queue<GameObject>> dictionary, string tag) => dictionary[tag].Count <= 0 ? null : dictionary[tag].Dequeue();

        public static Vector3 GetMousePositionY(Camera camera, Vector3 mousePosition)
        {
            Vector3 mouseWorld = camera.WorldToScreenPoint(mousePosition);
            mouseWorld.x = 0f;
            mouseWorld.z = 0f;

            return mouseWorld;
        }
        public static void ShuffleList<T>(this IList<T> list)
        {
            System.Random rng = new System.Random();

            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static double AreaCalculation(double[,] coordinates)
        {
            double sum = 0;
            int queue = 0;

            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                double x1;
                double x2;

                double Y = coordinates[i, 1];

                if (queue == 0)
                {
                    x1 = coordinates[i + 1, 0];
                    x2 = coordinates[coordinates.GetLength(0) - 1, 0];
                }
                else if (queue == coordinates.GetLength(0) - 1)
                {
                    x1 = coordinates[0, 0];
                    x2 = coordinates[i - 1, 0];
                }
                else
                {
                    x1 = coordinates[i + 1, 0];
                    x2 = coordinates[i - 1, 0];
                }

                sum += Y * (x1 - x2);

                queue++;
            }

            return Math.Abs(sum / 2);
        }
        public static float AngleBetweenTwoVectors(Vector3 from, Vector3 to)
        {
            float angle = Vector3.Angle(Vector3.forward, to - from);

            if (from.x > to.x)
            {
                angle = 360 - angle;
            }

            return angle;
        }

        #region Private Methods
        private static float CalculateFloat(float minValue, float maxValue) => UnityEngine.Random.Range(minValue, maxValue);
        private static int CalculateInt(int minValue, int maxValue) => UnityEngine.Random.Range(minValue, maxValue);
        private static T SortListAndGetElement<T>(List<T> list, int index)
        {
            list.Sort();
            return list[index];
        }
        #endregion Private Methods
    }
}