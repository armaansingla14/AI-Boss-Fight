# 🤖⚔️ AI Boss Fight

![Unity](https://img.shields.io/badge/Unity-2022.3.12f1-000000?logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![SharpNEAT](https://img.shields.io/badge/AI-SharpNEAT_Neuroevolution-8A2BE2)

A 2D combat game built in **Unity** where you battle a boss that isn't scripted — it **evolves**. Using the **SharpNEAT** neuroevolution framework, the AI boss breeds and mutates neural networks across generations, learning to read the player and counter their strategy like a human opponent would.

---

## 🧠 How the AI Works

Instead of hand-authored behavior trees, the boss is a **neural network evolved with NEAT** (NeuroEvolution of Augmenting Topologies). Each boss is a "unit" whose brain is a black box the game feeds sensor data into and reads actions out of.

### Sensing → Thinking → Acting
Defined in [`Assets/Scripts/EnemyController.cs`](Assets/Scripts/EnemyController.cs), which extends `UnitController`:

- **Inputs (5):** the player's X position, the boss's own X position, and which zone the player is attacking — so the network can reason about spacing and incoming threats.
- **Outputs (2):** movement / action decisions driving [`EnemyMovement`](Assets/Scripts/EnemyMovement.cs).
- **Fitness:** rewards staying near the player and dealing damage while avoiding taking it — computed from proximity and health deltas each evaluation.

### Evolution loop
[`Assets/UnitySharpNEAT/NeatSupervisor.cs`](Assets/UnitySharpNEAT/NeatSupervisor.cs) is the entry point for evolution — it spawns a population of boss brains, evaluates each over trials, and runs the generational NEAT algorithm so the topology and weights improve over time.

### Training opponent
[`Assets/Scripts/Player Dummy AI.cs`](Assets/Scripts/Player%20Dummy%20AI.cs) provides scripted "dummy" players with tunable **Smartness** levels (from a passive training dummy up to random and reactive movement/attacks), giving the boss a curriculum to evolve against before it ever faces a human.

---

## 🕹️ Gameplay Features

- 2D arena-style melee combat with movement, jumping, and multi-zone attacks
- A boss that adapts across generations instead of following fixed patterns
- Health, attack, and UI systems built in C# ([`Assets/UI/`](Assets/UI/), [`Assets/Scripts/EntityHealth.cs`](Assets/Scripts/EntityHealth.cs))
- Raycast/attack debug visualizers for tuning behavior

---

## 🗂️ Project Structure

```
├── Assets/
│   ├── Scripts/           # Gameplay + NEAT unit controller (Enemy, Player, Health, Attack)
│   ├── UnitySharpNEAT/    # SharpNEAT neuroevolution engine + NeatSupervisor
│   ├── UI/                # Health & attack UI
│   └── Medieval King Pack 2/  # Sprites & animations
├── ProjectSettings/       # Unity project configuration
├── Packages/              # Unity package manifest
└── README.md
```

---

## 🚀 Getting Started

1. **Clone the repo**
   ```bash
   git clone https://github.com/armaansingla14/AI-Boss-Fight.git
   ```
2. **Open in Unity** — add the project in Unity Hub and open it with **Unity 2022.3.12f1** (or a compatible 2022.3 LTS build).
3. **Play & train** — enter Play mode; the `NeatSupervisor` drives evolution while you (or the dummy AI) provide the opposition. Watch the boss get harder to beat over generations.

---

## 📦 Dependencies

- **Unity 2022.3.12f1** (2022.3 LTS)
- **UnitySharpNEAT / SharpNEAT** — bundled in `Assets/UnitySharpNEAT/`
- .NET / Mono runtime shipped with Unity

---

## 🎮 Inspiration

Originally built as a collaborative AI design project, then cleaned up to showcase the core idea: **evolutionary machine learning applied to real-time gameplay**, where the challenge comes from a neural network that genuinely learns rather than a difficulty slider.

---

## 👤 Author

**Armaan Singla** — Computer Engineering @ Queen's University
[GitHub](https://github.com/armaansingla14)
