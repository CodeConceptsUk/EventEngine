# EventEngine

[![Build status](https://ci.appveyor.com/api/projects/status/s43g0nu23aqks25i/branch/master?svg=true)](https://ci.appveyor.com/project/m3zercat/eventengine/branch/master)

CodeConcepts EventEngine is a library to provide an implementation of event sourcing that should be easily reusable, including event versioning and many other goodies! Woo

<pre>
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
+=======================+==========================================================
</pre>

When creating events you should make any property that is a non-nullable type into a nullable type to guarantee that data within that event when evaluating is what is expected and not a default. 
