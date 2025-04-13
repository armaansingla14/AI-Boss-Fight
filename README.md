# AI-Boss-Fight

A 2D game developed in Unity using the SharpNEAT neuroevolution framework, where the player battles an AI-controlled boss. The boss adapts its behavior over time by learning from the player’s actions, effectively mimicking a human opponent through evolutionary strategies.

---

## 🧠 How It Works

This project leverages **SharpNEAT**, an evolutionary neural network algorithm, to train the AI boss based on fitness functions and observed player behavior. Over multiple generations, the boss evolves to counter player strategies.

---

## 🕹️ Gameplay Features

- 2D arena-style combat
- AI boss trained with SharpNEAT to simulate human-like responses
- Game loop includes win/loss tracking and adaptive difficulty
- Visual effects, movement, and attack patterns defined in Unity’s C# scripts

---

## 🗂️ Project Structure

```
├── Assets/              # All Unity assets including scripts, scenes, sprites
├── Packages/            # Unity package dependencies
├── BuildOutput/         # Game build output files
├── ProjectSettings/     # Unity project configuration
├── .vscode/             # IDE settings (optional)
├── .gitignore
├── .vsconfig
└── README.md
```

---

## 🚀 Getting Started

1. **Clone the Repository**
```bash
git clone https://github.com/armaansingla14/AI-Boss-Fight.git
```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add Project"
   - Select this folder to open the game in Unity

3. **Play & Train**
   - Play as the human player
   - The AI boss will train over generations and adapt

---

## 📦 Dependencies

- Unity (recommended version: 2020.3 LTS or newer)
- SharpNEAT (integrated via C# scripts)
- .NET Framework for Unity

---

## 👤 Author

Armaan Singla  
Computer Engineering @ Queen's University  
[GitHub Profile](https://github.com/armaansingla14)

---

## 🎮 Inspiration

Originally built as a collaborative AI design project, now simplified and cleaned to showcase core ideas in evolutionary machine learning applied to gameplay.
