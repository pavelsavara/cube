# Cube Data Model

This document details the different representations of the Rubik's Cube state that will be implemented in the `Cube.Model` .NET assembly. The core design principle is to have multiple, inter-convertible models, each optimized for a specific task. The three core models will be implemented as separate C# structs to ensure type safety and clarity.

## 1. `Cube` (Human-Friendly Model)

*   **Description:** A direct, intuitive representation of the cube's surface. It will consist of an array of 54 `Color` values, one for each sticker. This model is primarily for visualization and debugging.
*   **Implementation Details:**
    *   A `Facelet` enum will define the 54 sticker positions (e.g., `UBL`, `FRD`) to be used as indices.
    *   A `Color` enum will define the 6 face colors (e.g., `White`, `Green`, `Red`).
    *   The model itself will be a `struct Cube` containing a `Color[54]` array.
*   **Use Case:** Rendering the cube in a UI, printing the cube state to the console for debugging.

## 2. `CubeIndex` (Permutation/Indexable Model)

*   **Description:** A compact, canonical representation based on the mathematical state of the cube's pieces. This model describes the position and orientation of each corner and edge piece. This is considered the canonical state from which others are derived.
*   **Implementation Details:**
    *   The model will be a `struct CubeIndex`.
    *   **Corner Pieces:**
        *   `CornerPermutation[8]`: An array where the index represents the corner *position* and the value represents which corner *piece* is in that position.
        *   `CornerOrientation[8]`: An array where the index represents the corner position and the value (0, 1, or 2) represents its twist.
    *   **Edge Pieces:**
        *   `EdgePermutation[12]`: An array where the index represents the edge *position* and the value represents which edge *piece* is in that position.
        *   `EdgeOrientation[12]`: An array where the index represents the edge position and the value (0 or 1) represents its flip.
*   **Indexing Strategy:**
    *   To generate a unique integer index for any cube state, we will implement the **linear time O(n) algorithm for ranking and unranking permutations by Myrvold and Ruskey**.
    *   We will compute separate ranks for the corner permutation, edge permutation, corner orientation, and edge orientation, and then combine them into a single, unique index.
*   **Use Case:** Storing and retrieving states from lookup tables, such as pattern databases or transposition tables during a search algorithm.

## 3. `CubeFeatures` (DNN Input Model)

*   **Description:** A flat numerical vector specifically engineered for the neural network. It combines raw sticker data with pre-processed features about piece correctness to accelerate learning.
*   **Implementation Details:**
    *   The model will be a `struct CubeFeatures` containing a `float[]` of size **364**.
    *   **Features 0-323 (324 total):** One-hot encoding of the 54 sticker colors (54 stickers × 6 colors).
    *   **Features 324-343 (20 total):** A binary flag (1.0f or 0.0f) for each of the 20 movable pieces (8 corners, 12 edges), indicating if its *position* is correct relative to the solved state.
    *   **Features 344-363 (20 total):** A binary flag (1.0f or 0.0f) for each of the 20 movable pieces, indicating if its *orientation* is correct.
*   **Use Case:** To be used as the `Features` column for the ML.NET model to predict the cost-to-go function.

## 4. Implementation and Testing Strategy

*   **Project:** All model structs, enums, and conversion logic will be located in a dedicated .NET class library project named `Cube.Model`.
*   **Conversions:** The structs will have constructors to allow for easy conversion. For example, `new Cube(cubeIndex)` and `new CubeFeatures(cubeIndex)`. This makes the transformation logic explicit and discoverable.
*   **Testing:**
    *   A dedicated unit test project (`Cube.Model.Tests`) will be created.
    *   **Rigorous testing of transformations is critical.** We will create a suite of tests that:
        1.  Applies a sequence of moves to a `CubeIndex` instance.
        2.  Converts the `CubeIndex` to a `Cube` and a `CubeFeatures`.
        3.  Asserts that the information is consistent across all models.
        4.  Converts the `Cube` back to a `CubeIndex` and asserts that no data was lost (round-trip conversion).
        5.  Tests edge cases, such as the solved state and states after a single move.