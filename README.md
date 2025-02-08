# Unity Reinforcement Learning for Autonomous Driving

This repository contains a **Reinforcement Learning (RL) powered Autonomous Driving** simulation built using **Unity ML-Agents**. The project trains an autonomous vehicle in a simulated environment to navigate roads, avoid obstacles, and optimize driving performance through reinforcement learning.

## Features
- **Unity ML-Agents Integration**: Uses Unity ML-Agents Toolkit for reinforcement learning.
- **Neural Network Training**: Implements deep reinforcement learning (DRL) to teach the vehicle driving behavior.
- **Obstacle Avoidance**: The agent learns to navigate dynamically generated obstacles.
- **Multiple Reward Functions**: Includes penalties for collisions and rewards for efficient pathing.
- **Simulation-Based Training**: The model is trained in Unity’s physics-based environment.

## Installation
### Prerequisites
- Unity (2021 or later recommended)
- Python (3.7+ required for ML-Agents)
- Unity ML-Agents Toolkit

### Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/Unity-RL-Autonomous-Driving.git
   cd Unity-RL-Autonomous-Driving
   ```
2. Install Unity ML-Agents package:
   ```bash
   pip install mlagents
   ```
3. Open the project in Unity Editor.
4. Set up ML-Agents environment in Unity.
5. Train the model using:
   ```bash
   mlagents-learn config/training_config.yaml --run-id=autonomous_vehicle
   ```

## Training Process
1. **Environment Setup**: The Unity simulation contains roads, intersections, and traffic elements.
2. **Agent Actions**: The vehicle can steer, accelerate, and brake based on learned policies.
3. **Reward System**:
   - Positive rewards for staying on track and following smooth driving patterns.
   - Negative rewards for collisions, driving off-road, or abrupt braking.
4. **Training Iterations**: The agent iteratively improves driving behavior through reinforcement learning.

## Usage
Once the model is trained, you can test it in Unity:
1. Navigate to the Unity scene with the trained agent.
2. Press **Play** to watch the vehicle drive autonomously.
3. Modify training parameters in `training_config.yaml` to refine behavior.

## Roadmap
- Implement multi-agent environments for interaction with other vehicles.
- Enhance obstacle recognition using LIDAR and camera-based perception.
- Extend to real-world datasets for enhanced realism.

