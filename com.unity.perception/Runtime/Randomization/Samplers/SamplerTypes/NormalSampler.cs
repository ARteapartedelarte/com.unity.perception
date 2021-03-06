﻿using System;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Experimental.Perception.Randomization.Samplers
{
    /// <summary>
    /// Returns normally distributed random values bounded within a specified range
    /// https://en.wikipedia.org/wiki/Truncated_normal_distribution
    /// </summary>
    [Serializable]
    public struct NormalSampler : ISampler
    {
        [SerializeField, HideInInspector] Unity.Mathematics.Random m_Random;

        /// <summary>
        /// The mean of the normal distribution to sample from
        /// </summary>
        public float mean;

        /// <summary>
        /// The standard deviation of the normal distribution to sample from
        /// </summary>
        public float standardDeviation;

        /// <summary>
        /// The base seed used to initialize this sampler's state
        /// </summary>
        [field: SerializeField] public uint baseSeed { get; set; }

        /// <summary>
        /// The current random state of this sampler
        /// </summary>
        public uint state
        {
            get => m_Random.state;
            set => m_Random = new Unity.Mathematics.Random { state = value };
        }

        /// <summary>
        /// A range bounding the values generated by this sampler
        /// </summary>
        [field: SerializeField]
        public FloatRange range { get; set; }

        /// <summary>
        /// Constructs a normal distribution sampler
        /// </summary>
        /// <param name="min">The smallest value contained within the range</param>
        /// <param name="max">The largest value contained within the range</param>
        /// <param name="mean">The mean of the normal distribution to sample from</param>
        /// <param name="standardDeviation">The standard deviation of the normal distribution to sample from</param>
        /// <param name="baseSeed">The base random seed to use for this sampler</param>
        public NormalSampler(
            float min, float max, float mean, float standardDeviation, uint baseSeed=SamplerUtility.largePrime)
        {
            range = new FloatRange(min, max);
            this.mean = mean;
            this.standardDeviation = standardDeviation;
            this.baseSeed = baseSeed;
            m_Random.state = baseSeed;
        }

        /// <summary>
        /// Resets a sampler's state to its base random seed
        /// </summary>
        public void ResetState()
        {
            state = baseSeed;
        }

        /// <summary>
        /// Deterministically offsets a sampler's state
        /// </summary>
        /// <param name="offsetIndex">
        /// The index used to offset the sampler's state.
        /// Typically set to either the current scenario iteration or a job's batch index.
        /// </param>
        public void IterateState(int offsetIndex)
        {
            state = SamplerUtility.IterateSeed((uint)offsetIndex, state);
        }

        /// <summary>
        /// Generates one sample
        /// </summary>
        /// <returns>The generated sample</returns>
        public float Sample()
        {
            return SamplerUtility.TruncatedNormalSample(
                m_Random.NextFloat(), range.minimum, range.maximum, mean, standardDeviation);
        }

        /// <summary>
        /// Schedules a job to generate an array of samples
        /// </summary>
        /// <param name="sampleCount">The number of samples to generate</param>
        /// <param name="jobHandle">The handle of the scheduled job</param>
        /// <returns>A NativeArray of generated samples</returns>
        public NativeArray<float> Samples(int sampleCount, out JobHandle jobHandle)
        {
            var samples = SamplerUtility.GenerateSamples(this, sampleCount, out jobHandle);
            IterateState(sampleCount);
            return samples;
        }
    }
}
