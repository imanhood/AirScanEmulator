import sys
import numpy as np
from sklearn.cluster import KMeans

import pandas as pd

def read_points_from_csv(file_path):
    df = pd.read_csv(file_path, header=None)
    points = [tuple(row) for row in df.values]
    return points

def find_optimal_k(points, max_k):
    # Convert points to numpy array
    points_array = np.array(points)

    sum_of_squared_distances = []
    list_kmeans = []
    K_range = range(1, max_k + 1)

    for k in K_range:
        kmeans = KMeans(n_clusters=k)
        kmeans.fit(points_array)
        list_kmeans.append(kmeans)
        sum_of_squared_distances.append(kmeans.inertia_)
    m = 0
    p = sum_of_squared_distances[0] * 1.1
    k = 0
    the_k = 0
    for d in sum_of_squared_distances:
        k+=1
        if p / d > m:
            m = p / d
            the_k = k
        p = d
    
    if(m < 3):
        the_k = 1
    return list_kmeans[the_k - 1]

if __name__ == "__main__":
    # Read points from command-line arguments
    #points = [[float(x), float(y)] for x, y in [arg.split(',') for arg in sys.argv[1].split(';')]]
    points = read_points_from_csv('C:\\Users\\ikazemi\\source\\repos\\AirScanEmulator\\AirScanEmulator\\scripts\\py\\points.csv');

    # Find the optimal number of clusters using the elbow method
    optimal_kmeans = find_optimal_k(points, max_k=10)
    
    # Output the optimal value
    print(optimal_kmeans.cluster_centers_.tolist())
