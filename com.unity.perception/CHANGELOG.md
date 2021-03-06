# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## Unreleased

### Added
Added support for labeling Terrain objects. Trees and details are not labeled but will occlude other objects.
Added instance segmentation labeler
Added support for full screen visual overlays and overlay manager

### Changed

Updated perception to use burst 1.3.9
Changed InstanceSegmentationImageReadback event to provide a NativeArray\<Color32\> instead of NativeArray\<uint\>
Expanded all Unity Simulation references from USim to Unity Simulation
Uniform and Normal samplers now serialize their random seeds

The ScenarioBase's GenerateIterativeRandomSeed() method has been renamed to GenerateRandomSeedFromIndex()

### Deprecated

### Removed

### Fixed

UnitySimulationScenario now correctly deserializes app-params before offsetting the current scenario iteration when executing on Unity Simulation

Fixed Unity Simulation nodes generating one extra empty image before generating their share of the randomization scenario iterations

Fixed enumeration in the CategoricalParameter.categories property

The GenerateRandomSeedFromIndex method now correctly hashes the current scenario iteration into the random seed it generates

## [0.5.0-preview.1] - 2020-10-14

### Known Issues

Creating a new 2020.1.x project and adding the perception package to the project causes a memory error that is a [known issue in 2020.1 editors](https://issuetracker.unity3d.com/issues/wild-memory-leaks-leading-to-stackallocator-walkallocations-crashes). Users can remedy this issue by closing and reopening the editor.

### Added

Added Randomizers and RandomizerTags
Added support for generating 3D bounding box ground truth data

### Changed

### Deprecated

### Removed

Removed ParameterConfigurations (replaced with Randomizers)

### Fixed

Fixed visualization issue where object count and pixel count labelers were shown stale values
Fixed visualization issue where HUD entry labels could be too long and take up the entire panel

## [0.4.0-preview.1] - 2020-08-07

### Added

Added new experimental randomization tools

Added support for 2020.1

Added Labeling.RefreshLabeling(), which can be used to update ground truth generators after the list of labels or the renderers is changed

Added support for renderers with MaterialPropertyBlocks assigned to individual materials

### Changed

Changed the way realtime visualizers rendered to avoid rendering conflicts

Changed default labeler ids to be lower-case to be consistent with the ids in the dataset

Switched to latest versions of com.unity.simulation.core and com.unity.simulation.capture

### Deprecated

### Removed

### Fixed

Fixed 2d bounding boxes being reported for objects that do not match the label config.

Fixed a categorical parameter UI error in which deleting an individual option would successfully remove the option from the UI but only serialize the option to null during serialization instead of removing it

Fixed the "Application Frequency" parameter UI field not initializing to a default value

Fixed the IterateSeed() method where certain combinations of indices and random seeds would produce a random state value of zero, causing Unity.Mathematics.Random to throw an exception

Fixed labeler editor to allow for editing multiple labelers at a time

Fixed labeler editor to ensure that when duplicating prefabs all labeler entries are also duplicated

Fixed colors in semantic segmentation images being darker than those specified in the label config

Fixed objects being incorrectly labeled when they do not match any entries in the label config

Fixed lens distortion in URP and HDRP now being applied to ground truth

### Security

## [0.3.0-preview.1] - 2020-08-07

### Added

Added realtime visualization capability to the perception package.

Added visualizers for built-in labelers: Semantic Segmentation, 2D Bounding Boxes, Object Count, and Rendered Object Info.

Added references to example projects in manual.

Added notification when an HDRP project is in Deferred Only mode, which is not supported by the labelers.

### Changed

Updated to com.unity.simulation.capture version 0.0.10-preview.10 and com.unity.simulation.core version 0.0.10-preview.17

Changed minimum Unity Editor version to 2019.4

### Fixed

Fixed compilation warnings with latest com.unity.simulation.core package.

Fixed errors in example script when exiting play mode

## [0.2.0-preview.2] - 2020-07-15

### Fixed

Fixed bug that prevented RGB captures to be written out to disk
Fixed compatibility with com.unity.simulation.capture@0.0.10-preview.8

## [0.2.0-preview.1] - 2020-07-02

### Added

Added CameraLabeler, an extensible base type for all forms of dataset output from a camera.
Added LabelConfig\<T\>, a base class for mapping labels to data used by a labeler. There are two new derived types - ID label config and semantic segmentation label config.

### Changed

Moved the various forms of ground truth from PerceptionCamera into various subclasses of CameraLabeler.
Renamed SimulationManager to DatasetCapture.
Changed Semantic Segmentation to take a SemanticSegmentationLabelConfig, which maps labels to color pixel values.

## [0.1.0] - 2020-06-24

### This is the first release of the _Perception_ package
