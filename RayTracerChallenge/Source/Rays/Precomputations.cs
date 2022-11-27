﻿using RT.Source.Figures;
using RT.Source.Vectors;

namespace RT.Source.Rays
{
    public struct Precomputations
    {
        // Copy the intersections properties, for convenience
        public readonly float T;
        public readonly Figure figure;

        // Precomputed values
        public readonly Point point;
        public readonly Vector eyeV;
        public readonly Vector normalV;
        public readonly bool inside;  // Is intersection inside of an object

        public Precomputations(Intersection i, Ray r)
        {
            T = i.T;
            figure = i.figure;

            point = r.Position(T);
            eyeV = new(-r.direction);
            normalV = figure.NormalAt(point);

            inside = Vector.DotProduct(normalV, eyeV) < 0;
            if (inside)
                normalV = new(-normalV);
        }
    }
}
