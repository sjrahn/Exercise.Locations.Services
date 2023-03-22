# Locations Services Microservice Exercise

Fork repository & design a microservice that needs to handle geojson data for the purposes for storing and querying locations

An example of possible data that would be ingested
```
 "type": "FeatureCollection",
  "features": [
      {
          "type": "Feature",
          "properties": {
              "DLSSHORT": "13-6-69-8-W6",
              "QUARTER": "13",
              "SECTION": "6",
              "TOWNSHIP": "69",
              "RANGE": "8",
              "MERIDIAN": "W6",
              "PROVINCE": "Alberta",
              "COUNTY": "Greenview No. 16",
              "LATITUDE": 54.95063,
              "LONGITUDE": -119.217669,
              "ZOOM": 14,
              "MGRS": "11U LA 57976 91548",
              "UTM": "11N 357977 6091548",
              "NTS": "A-48-J/83-L-14",
              "PID": "60806906130"
          },
          "geometry": {
              "type": "Polygon",
              "coordinates": [
                  [
                      [
                          -119.214525,
                          54.948821
                      ],
                      [
                          -119.214524,
                          54.95244
                      ],
                      [
                          -119.220816,
                          54.952439
                      ],
                      [
                          -119.220813,
                          54.948819
                      ],
                      [
                          -119.214525,
                          54.948821
                      ]
                  ]
              ]
          }
      }
  ]
  ```
  
  However the service should be able to handle any geojson data that describes a physical location https://geojson.org/
  
  Provide endpoints to do the following:
  - [ ] persist new location to a datastore
  - [ ] query if point coordiate falls within or approximate to a location within a parameterized distance
  
  Either create a pull request back for review or invite us in to review the service
  
