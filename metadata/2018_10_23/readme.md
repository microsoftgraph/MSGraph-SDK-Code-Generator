# New functionality 

**itemPreviewInfo** complexType added. It is used by the **preview** action bound to the **driveitem** entity.

## Steps to produce
1. [Downloaded metadata](https://gist.github.com/MIchaelMainer/4b70a6d57856845d34d8d69bf89e1c32) from v1.0/$metadata on 10/23/2018.
2. Formatted the metadata with 2 spaces and named it v1.0_2018_10_23_source.xml. 
3. Removed all of the capability annotations and added documentation annotations. This results in **v1.0_2018_10_23_descriptions.xml which we will use with the code generator**. There were no new documentation annotations added.

## Beta metadata
beta_2018_10_23_descriptions.xml contains the formatted raw metadata from the live site.