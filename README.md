# EventEngine
CodeConcepts EventEngine is a library to provide an implementation of event sourcing that should be easily reusable, including event versioning and many other goodies! Woo

+=======================+==========================================================
|Type					| Description
+=======================+==========================================================
|Command				| An instruction 
|Command Handler		| Creates one or more events from a command
|Event					| Something that has happened
|Event Handler			| Mechanism to interpret an event in the context of a View
|Event Store			| Repository to store and retrieve events
|Event Type				| Type of an event and version it is associated with
|View Query				| Uses matching events to create a View
|View					| A representation of event data