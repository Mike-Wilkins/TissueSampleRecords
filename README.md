# TissueSampleRecords
ASP.NET Core Application

## Project Overview
Tech Events Manager is an ASP.NET MVC application which implements GoogleMaps API and Geocoding services as a tool for enabling users to locate tech meetup events in their local area. After entering a valid UK postcode, users can filter events according to proximity from their home address. The application implements an administrative service for managing new events, editing and deleting expired events.

## GoogleMaps & Geocoding
* When creating a new event, conversion from postcode to lat/lng is handled using Google Geocoding services and is implemented prior to submitting the entry to the database table. This addresses potential problems occuring due to the restricted number of Geocoding requests limiting 50 per second.
* The proximity filter is implemented using a Havensine formular which is applied prior to rendering each marker on the map. The code for this operation can be found in the home index view file which calculates the distance between postcodes entered by the user and event postcodes stored on the database. The filtering process is implemented using conditional logic which restricts the rendering of markers based on the distance defined by the user. 

![Output sample](https://github.com/Mike-Wilkins/TissueSampleRecords/blob/master/4ja5xj.gif)
