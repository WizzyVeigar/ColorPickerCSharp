using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ArduinoColorPicker
{
    /// <summary>
    /// Calculates the K-Means Clusters for a set of colours
    /// </summary>
    public class KMeansClusteringCalculator
    {
        /// <summary>
        /// Calculates the <paramref name="k"/> clusters for <paramref name="colours"/>. Iterations continues until clusters move by less than <paramref name="threshold"/>
        /// </summary>
        /// <param name="k">The number of clusters to calculate (eg. The number of results to return)</param>
        /// <param name="colours">The list of colours to calculate <paramref name="k"/> for</param>
        /// <param name="threshold">Threshold for iteration. A lower value should produce more correct results but requires more iterations and for some <paramref name="colours"/> may never produce a result</param>
        /// <returns>The <paramref name="k"/> colours for the image in descending order from most common to least common</returns>
        public IList<Color> Calculate(int k, IList<Color> colours, double threshold = 0.0d)
        {
            List<KCluster> clusters = new List<KCluster>();

            // 1. Initialisation.
            //   Create K clusters with a random data point from our input.
            //   We make sure not to use the same index twice for two inputs
            Random random = new Random();
            List<int> usedIndexes = new List<int>();
            while (clusters.Count < k)
            {
                int index = random.Next(0, colours.Count);
                if (usedIndexes.Contains(index) == true)
                {
                    continue;
                }

                usedIndexes.Add(index);
                KCluster cluster = new KCluster(colours[index]); //at this point cluster has a random color out of all the pixels in the picture
                clusters.Add(cluster);                           //adds that clusterColor to a list of clusters
            }

            bool updated = false;
            do
            {
                updated = false;
                // 2. For each colour in our input determine which cluster's centre point is the closest and add the colour to the cluster
                foreach (Color colour in colours)
                { //Loops through all the colors in the picture (colours)
                    double shortestDistance = float.MaxValue;
                    KCluster closestCluster = null;
                    //Loops the color through the amount of clusters and finds the cluster with the closest color(Distance/Euclidean Distance)
                    foreach (KCluster cluster in clusters)
                    {
                        double distance = cluster.DistanceFromCentre(colour);
                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                            closestCluster = cluster;
                        }
                    }

                    closestCluster.Add(colour); //Closestcluster now holds the color that was closest to the color in the cluster list
                }

                // 3. Recalculate the clusters centre.
                foreach (KCluster cluster in clusters)
                {
                    if (cluster.RecalculateCentre(threshold) == true)
                    {
                        updated = true;
                    }
                }

                // 4. If we updated any centre point this iteration then iterate again
                //Checks if the cluster centres need to recalculate again, based on threshold being false. 
            } while (updated == true);
            return clusters.OrderByDescending(c => c.PriorCount).Select(c => c.Centre).ToList();
        }
    }
}