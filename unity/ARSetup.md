# AR Lab Setup (Unity)

1) Create Unity project (2022.3+ LTS recommended). Install:
- AR Foundation, ARCore XR Plugin (via Package Manager).
- Vuforia Engine (via Vuforia Hub/Package). Add your Vuforia License Key in Project Settings > Vuforia.

2) Scenes:
- MainMenu: Canvas with buttons wired to MainMenuUI.
- LabManual: Canvas with TMP texts and Start button -> LabManualUI (assign ExperimentDefinition ScriptableObject or use ExperimentBootstrap.GetOrCreate()).
- ARExperiment: 
  - ARSession, ARSessionOrigin (with ARRaycastManager), ARCamera.
  - PlaneManager if you want plane detection.
  - GameObjects: SampleCylinder, StandardCylinder (each with mesh for liquid with material using ARLab/UnlitTurbidity shader).
  - Add TurbidityController to each liquid mesh; reference them in StepEngine.
  - Add StirController and ARObjectController; link ARCamera and a default selectedObject (e.g., the stand).
  - Voice: add EditorLogVoiceService in Editor, AndroidTTSService on device.
  - UI Canvas: Step text (TMP), Next button, Progress slider, and ObservationTable panel (initially disabled).
- Quiz: Canvas with texts and option buttons wired to QuizManager. Place quiz-questions.json in Resources folder.

3) UI Theme:
- Use Theme asset colors: Primary #0EA5E9, NeutralBg #FFFFFF, NeutralText #111827, Muted #6B7280, Accent #10B981 (5 colors total). Use a single sans font (e.g., Roboto) for headings/body.

4) Build:
- Android target, IL2CPP or Mono. Min API 24+. Add ARCore Required in XR settings.
- Add all scenes to Build Settings in order: MainMenu, LabManual, ARExperiment, Quiz.
