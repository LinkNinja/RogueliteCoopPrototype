# Devlog — Milestone 1 Complete (Networking Foundation & Player Movement)
**Date:** Feb 13, 2026  
**Milestone:** Milestone 1 — Networking Foundation

## Overview
Wrapped up the entire networking foundation for the multiplayer roguelite prototype. This milestone focused on establishing a clean, stable baseline for all future systems: scene flow, player spawning, input handling, and networked movement. The goal was to ensure the project had a professional, scalable architecture before moving into gameplay systems.

---

## What I Accomplished

### 1. Project Setup & Architecture
- Set up Unity 6 LTS with URP  
- Established a clean folder structure and naming conventions  
- Created Bootstrap and Gameplay scenes  
- Implemented a professional, modular project layout  

### 2. Networking Foundation (Photon Fusion)
- Integrated Photon Fusion 2.0.11  
- Implemented `NetworkRunner` setup  
- Added `NetworkCallbacks` with correct callback signatures  
- Ensured proper scene loading flow from Bootstrap → Gameplay  
- Fixed issues with incorrect scene loading and callback mismatches  

### 3. Player Spawning
- Created Player prefab with:
  - `NetworkObject`  
  - `CharacterController`  
  - `NetworkTransform`  
  - `PlayerController` (movement logic)  
- Implemented networked player spawning for both Host and Client  
- Verified independent player instances for each client  

### 4. Input Handling
- Implemented Fusion’s `OnInput` callback  
- Set up `PlayerInputActionsSingleton` for input collection  
- Confirmed input authority logic  
- Ensured input is passed correctly to the PlayerController  

### 5. Networked Movement
- Implemented PlayerController using CharacterController movement  
- Switched movement authority to `HasInputAuthority` for proper client control  
- Verified smooth, independent movement on both Host and Client  
- Confirmed NetworkTransform syncing across clients  

---

## Problems I Encountered
- Scene loading issues caused by starting in the wrong scene  
- Fusion callback signature mismatches due to version differences  
- Player movement not working because PlayerController wasn’t attached  
- Input authority confusion (StateAuthority vs InputAuthority)  
- Missing CharacterController and NetworkTransform on the Player prefab  

---

## How I Solved Them
- Forced Gameplay scene index to load correctly  
- Updated all callbacks to match Fusion 2.0.11  
- Rebuilt Player prefab from a clean baseline  
- Added CharacterController + NetworkTransform  
- Switched movement logic to use InputAuthority  
- Verified input flow through debug logs  
- Tested with Host build + Editor client to confirm correct behavior  

---

## What I Learned
- Fusion does not sync transforms automatically — NetworkTransform is required  
- InputAuthority controls movement; StateAuthority controls simulation  
- Scene loading order is critical in networked projects  
- Clean prefab setup prevents most networking issues  
- Always test with two clients early to catch authority problems  

---

## Next Steps (Milestone 2 — Player Systems)
- Implement PlayerStats (health, damage, movement modifiers)  
- Add PlayerCombat (basic attack system)  
- Add Downed state  
- Implement revive mechanic  
- Add local hit feedback  
- Add health UI  

This milestone will introduce the first real gameplay loop and give the player an identity beyond movement.