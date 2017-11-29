using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeftEngine
{
    public class Neuron
    {
        public int numInputs;
        public float[] weights;

        public Neuron(int numInputs)
        {
            this.numInputs = numInputs + 1; // + 1 for bias
            weights = new float[this.numInputs];

            RandomiseWeights();
        }

        private void RandomiseWeights()
        {
            for (int i = 0; i < weights.Length; ++i)
                weights[i] = DeftEngine.RandFloat1Neg1();
        }
    }

    public class NeuronLayer
    {
        public int numNeurons;
        public int inputsPerNeuron;
        public List<Neuron> neurons;

        public NeuronLayer(int numNeurons, int inputsPerNeuron)
        {
            this.numNeurons = numNeurons;
            this.inputsPerNeuron = inputsPerNeuron;

            PopulateNeurons();
        }

        private void PopulateNeurons()
        {
            neurons = new List<Neuron>();

            for (int i = 0; i < numNeurons; ++i)
                neurons.Add(new Neuron(inputsPerNeuron));
        }
    }

    public class NeuralNet
    {
        public List<float> inputs;
        public List<float> outputs;


        public List<NeuronLayer> layers;

        public NeuralNet()
        {
            layers = new List<NeuronLayer>();
            inputs = new List<float>();
            outputs = new List<float>();
        }

        public float Sigmoid(double x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        public void Randomise()
        {
            layers = new List<NeuronLayer>();

            // Create first hidden layer.
            // Input count == Num_Inputs, since input for this layer comes from input layer.
            layers.Add(new NeuronLayer(SimParams.NUM_NEURONS_PER_HIDDEN_LAYER, SimParams.NUM_INPUTS));

            // Create other hidden layers.
            // Input count == Neurons_Per_Hidden_layer, since input comes from a different hidden layer.
            for (int i = 0; i < SimParams.NUM_HIDDEN_LAYERS - 1; ++i)
            {
                layers.Add(new NeuronLayer(SimParams.NUM_NEURONS_PER_HIDDEN_LAYER, SimParams.NUM_NEURONS_PER_HIDDEN_LAYER));
            }

            // Create output layer.
            // Neuron count == NUM_OUTPUTS
            layers.Add(new NeuronLayer(SimParams.NUM_OUTPUTS, SimParams.NUM_NEURONS_PER_HIDDEN_LAYER));
        }

        public List<float> GetWeights()
        {
            var result = new List<float>();

            foreach (var layer in layers)
            {
                foreach (var neuron in layer.neurons)
                {
                    foreach (var weight in neuron.weights)
                        result.Add(weight);
                }
            }

            return result;
        }

        public int GetNumWeights()
        {
            int result = 0;

            foreach (var layer in layers)
            {
                foreach (var neuron in layer.neurons)
                {
                    foreach (var weight in neuron.weights)
                        result += 1;
                }
            }

            return result;
        }

        public void ReplaceWeights(List<float> weights)
        {
            int curWeight = 0;

            foreach (var layer in layers)
            {
                foreach (var neuron in layer.neurons)
                {
                    for (int i = 0; i < neuron.numInputs; ++i)
                    {
                        neuron.weights[i] = weights[curWeight];
                        curWeight += 1;
                    }
                }
            }
        }

        // NOTE: Refer to github.com/Plottel/Shooters--NeuralNetwork for
        // COMMENTED Python code.
        public void Run()
        {
            outputs = new List<float>();

            foreach (var layer in layers)
            {
                // Runs on all layers except first.
                if (layer.inputsPerNeuron != SimParams.NUM_INPUTS)
                    inputs = outputs;

                outputs = new List<float>();
                int curWeight = 0;

                foreach (var neuron in layer.neurons)
                {
                    float totalWeightedInput = 0;

                    for (int i = 0; i < neuron.numInputs - 1; ++i)
                    {
                        totalWeightedInput += inputs[curWeight] * neuron.weights[i];
                        ++curWeight;
                    }

                    totalWeightedInput += neuron.weights.Last() * SimParams.BIAS;

                    outputs.Add(Sigmoid(totalWeightedInput));
                    curWeight = 0;
                }
            }
        }
    }
}
