SignalEV
========

Publishes events from the server the client in a reasonably strong typed manner using SignalR

The name was kind of derived from SignalR and Event. Could be pronounced [SignalEeewww] or [SignalEve]. Doesn't really matter does it?

Using it
--------

### Server

On the server side (Hub side) just publish an event using the `IHubContext` extension method `Publish<TEvent>(TEvent @event)`.

### Client

Register your event handling method using the `IHubProxy` extension method `RegisterGenericHandler(Type[] types, object instance, MethodInfo handlerInfo`).

TODO
----

- maybe change the `MemberInfo` parameter type to an `Expression` or something...
- 

Credits
=======

- possibly stole the idea from http://jasondentler.com/blog/2012/02/nservicebus-and-signalr/