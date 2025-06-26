# Project Plan: Deep Learning Rubik's Cube Solver (.NET Edition)

This document outlines the plan for creating a Rubik's Cube solver using deep learning to approximate "God's Algorithm". The core of this project is to train a classifier to estimate the number of steps (distance) from any given scrambled state to the solved state. This project will be built using the .NET ecosystem, with ML.NET for model training, Azure Machine Learning for scalable computation, and .NET Aspire for deployment orchestration.

## 1. Research and Knowledge Base

*   **Objective:** To understand the current state-of-the-art in solving Rubik's Cube with AI and deep learning.
*   **Tasks:**
    *   [x] Conduct initial research on existing approaches (e.g., DeepCubeA, OpenAI's work).
    *   [x] Create a knowledge base in `plan/knowledge.md` to document findings.
    *   [ ] Continuously update the knowledge base as the project progresses.

## 2. Data Model and Representation

*   **Objective:** Define how the Rubik's Cube state will be represented for the neural network.
*   **Key Considerations:**
    *   The representation should be suitable for the chosen neural network architecture.
    *   It should be efficient in terms of memory and computation.
*   **Proposed Representation:**
    *   Start with a one-hot encoding of the 54 stickers. Each sticker's color will be a one-hot vector. The entire cube state will be a concatenation of these vectors.
    *   Investigate other representations, such as the permutation and orientation of corner and edge pieces, as this might be more compact and informative.

## 3. Exploiting Symmetries

*   **Objective:** To reduce the problem space and improve model generalization by using the cube's symmetries.
*   **Tasks:**
    *   Identify the 48 symmetries of the Rubik's Cube.
    *   Implement functions to apply these symmetry transformations to a given cube state.
    *   Integrate data augmentation using symmetries into the training pipeline. For each training sample, generate symmetric states to expand the dataset.

## 4. Neural Network Architecture

*   **Objective:** To choose and design a deep neural network architecture for estimating the distance to the solved state, implemented in .NET.
*   **Technology Stack:**
    *   **Framework:** ML.NET or TorchSharp.
    *   **Rationale:** These libraries allow for the definition and training of deep neural networks directly within the .NET ecosystem. ML.NET offers a high-level API and integration with the ONNX format, while TorchSharp provides a lower-level, PyTorch-like experience for maximum flexibility. We will start with ML.NET and its TensorFlow or ONNX integration.
*   **Initial Architecture (based on DeepCubeA):**
    *   A Deep Neural Network (DNN) with fully connected layers and residual blocks.
    *   Input: One-hot encoded vector of the cube state.
    *   Output: A single value representing the estimated distance to solve.
*   **Tasks:**
    *   [ ] Set up a .NET project for the model.
    *   [ ] Implement the baseline DNN architecture using ML.NET.
    *   [ ] Research and potentially implement a GNN architecture if the initial model is not sufficient.

## 5. Training Pipeline Architecture

*   **Objective:** To design a scalable and efficient pipeline for training the model using cloud resources.
*   **Components:**
    *   **Data Generation (.NET Core Console App):** A module to generate scrambled cube states and their corresponding distances to the solved state. This will use Kociemba's algorithm or a breadth-first search for initial data.
    *   **Data Augmentation:** The symmetry transformation module.
    *   **Training Loop:** The main loop that feeds data to the model, calculates loss, and updates weights, implemented in our .NET training application.
    *   **Evaluation:** A module to evaluate the model's performance on a test set.
*   **Scalability and Cloud Training:**
    *   **Platform:** Azure Machine Learning (Azure ML).
    *   **Process:** The .NET training application will be containerized using Docker. This container will be submitted as a training job to an Azure ML compute cluster equipped with GPUs. This allows us to offload the heavy computation to the cloud and scale as needed.

## 6. Hyperparameter Tuning

*   **Objective:** To find the optimal hyperparameters for the neural network and training process.
*   **Hyperparameters to Tune:**
    *   Learning rate
    *   Batch size
    *   Number of layers and units in the network
    *   Optimizer parameters (e.g., momentum)
*   **Methodology:**
    *   Utilize Azure ML's hyperparameter tuning capabilities (e.g., Hyperdrive) to automate the search for optimal parameters on the cloud compute cluster.
    *   Use a validation set to evaluate different hyperparameter combinations.

## 7. Testing and Evaluation

*   **Objective:** To ensure the solution is correct, robust, and performs well.
*   **Testing Strategy:**
    *   **Unit Tests:** For individual components like data representation, symmetry transformations, and network layers.
    *   **Integration Tests:** To test the entire training pipeline.
    *   **Model Evaluation:**
        *   Measure the accuracy of the distance estimation on a held-out test set.
        *   Analyze the model's performance on cubes with different scramble depths.
        *   Once the distance estimator is trained, use it in a search algorithm (like A*) to actually solve the cube and measure the solution length and time.

## 8. Deployment with .NET Aspire

*   **Objective:** To create a robust, observable, and easily deployable application.
*   **Architecture:**
    *   **.NET Aspire AppHost:** An orchestrator project that defines the different components of our application.
    *   **Solver Service (ASP.NET Core API):** A web API project that loads the trained ONNX model and exposes an endpoint to solve a given cube state.
    *   **(Optional) Web Frontend (Blazor):** A Blazor Web App that provides a user interface for scrambling the cube and visualizing the solution.
*   **Benefits of Aspire:**
    *   **Simplified Development:** Easily run and debug the multi-project application locally.
    *   **Built-in Observability:** Automatic setup for logging, metrics, and tracing across all services.
    *   **Streamlined Deployment:** .NET Aspire provides tooling to generate manifests (e.g., for Azure Container Apps) to simplify deployment to the cloud.

## 9. TODO List

*   [ ] Set up the initial .NET solution structure with projects for the model, data generation, and an Aspire AppHost.
*   [ ] Implement the chosen data representation for the cube in C#.
*   [ ] Implement the symmetry transformation functions in C#.
*   [ ] Implement the baseline DNN architecture using ML.NET.
*   [ ] Create a Dockerfile for the training application.
*   [ ] Set up an Azure ML workspace and compute cluster.
*   [ ] Implement the data generation module.
*   [ ] Implement the training pipeline and submit a test run to Azure ML.
*   [ ] Set up a testing framework (e.g., xUnit) and write initial unit tests.
*   [ ] Create the ASP.NET Core Solver Service project within the Aspire solution.

## 10. Open Questions

*   What is the most efficient way to generate a large dataset of scrambled cubes with known distances to the solved state?
*   Which search algorithm will be used with the trained distance estimator to find the solution path? (e.g., A*, IDA*)
*   How will the final solver be packaged and presented? The current plan is an ASP.NET Core API orchestrated by .NET Aspire, potentially with a Blazor frontend. Is this sufficient?
