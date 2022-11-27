﻿using RT.Source.Figures;

namespace RT.Source.Rays
{
    public class Intersection : IComparable<Intersection>
    {
        public readonly float T;
        public readonly Figure figure;

        public Intersection(float T, Figure figure)
        {
            this.T = T;
            this.figure = figure;
        }

        public Precomputations PrepareComputations(Ray r) => new(this, r);

        public int CompareTo(Intersection? other) => other is null ? throw new ArgumentException("Comparing to null") : T.CompareTo(other.T);

        public override string ToString() => $"Intersection(T:{T}, Figure:{figure})";
    }
}
