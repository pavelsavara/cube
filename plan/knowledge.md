# Knowledge Base for Rubik's Cube Solver

This document contains the summary of research into existing approaches for solving Rubik's cube using deep learning.

## Existing Approaches

*   **DeepCubeA (2019):** A deep reinforcement learning approach by McAleer et al. that can find the shortest path to the goal state for the Rubik's Cube, and other combinatorial puzzles. It uses a novel path-finding algorithm called approximate value iteration. The model was trained for 44 hours on a 64-core machine. It uses a deep neural network to approximate the value function, which estimates the distance to the solved state.
*   **Solving the Rubik's Cube with Deep Reinforcement Learning (2018):** A paper by OpenAI that demonstrates a system that can solve the Rubik's cube with a robot hand. They used a technique called Automatic Domain Randomization (ADR) to train a neural network in simulation that can transfer to the real world. The policy is represented by a Kociemba solver, but the value function is learned.
*   **Estimating the distance to the goal state:** Many approaches use a neural network to estimate the cost-to-go (distance) from a given state. This is a regression problem where the input is the cube state and the output is a number. This is what the user is suggesting.

## Data Representation

*   The state of the Rubik's cube can be represented in various ways. A common representation is a one-hot encoded vector for each of the 54 colored squares (stickers) on the cube.
*   Another way is to represent the permutation and orientation of the corner and edge pieces.

## Symmetries

*   The Rubik's cube has a large symmetry group (43,252,003,274,489,856,000 states, but only about 519 quintillion unique configurations when considering symmetries).
*   Exploiting these symmetries can significantly reduce the size of the state space that the model needs to learn. For any given cube state, one can apply a symmetry transformation to get a new state that is equivalent in terms of distance to the solved state. During training, augmenting the dataset with these symmetric states can improve the model's generalization.

## Open Questions

*   What is the most effective neural network architecture for this problem? (e.g., Convolutional Neural Networks, Graph Neural Networks)
*   How to generate a good training dataset of scrambled cubes and their distances to the solved state?

## Data Generation Strategy

*   **Bootstrapping with Kociemba's Algorithm:** We can use an existing implementation of Kociemba's two-phase algorithm to generate a large number of scrambled cubes and their optimal solution lengths. This will provide a high-quality dataset for training the distance estimator.
*   **Breadth-First Search from Solved State:** For the first few levels of distance from the solved state (e.g., up to 5-7 moves), we can exhaustively generate all possible cube states by applying all possible move sequences. This will give us a complete and accurate dataset for short scramble depths. This can be done using a breadth-first search starting from the solved cube.
