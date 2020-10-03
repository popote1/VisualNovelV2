using System;
using System.Collections.Generic;

[Serializable]
public class Thumbnail {

    public int Id;
    public string Description;
    public string Background;
    public int GivenItem;
    public List<Choice> Choices;

}