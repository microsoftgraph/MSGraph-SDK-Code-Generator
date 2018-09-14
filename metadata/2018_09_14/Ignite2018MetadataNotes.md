# New functionality 

Add the Security Alerts APIs
Updates for Intune APIs. 
Updates on the User object including user insights.
Mailtips support on the user object.
Update to the Subscription entity.

## Steps to produce
1. [Downloaded metadata](https://gist.github.com/MIchaelMainer/4b70a6d57856845d34d8d69bf89e1c32) from v1.0/$metadata on 9/14/2018.
2. Formatted the metadata with 2 spaces and named it v1.0_2018_09_14_source.xml. 
3. Removed all of the annotations after the EntityContainer and named it v1.0_2018_09_14_source_noannots.xml.
4. Ran this through MetadataProcessor.exe and named the resulting file v1.0_2018_09_14_source_clean.xml. This puts the metadata in a state that we can use with the generator.
5. v1.0_2018_09_14_source_clean.xml submitted as input to LongDescription.exe which adds annotations from the documentation. The resulting file named v1.0_2018_09_14_descriptions.xml is the input for the code generator.

## Notes
renew function bound to educationClass entityType removed - it never worked.
educationSchool/administrativeUnits removed.
