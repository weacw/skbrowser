namespace SeekWhale
{
    public enum Inputtype
    {
        PHYSICS,
        AXIS,
    }
    public enum RenderingMode
    {
        OPAQUE,
        CUTOUT,
        FADE,
        TRANSPARENT
    }


    public enum Bundletype : int
    {
        MODEL,
        VIDEO,
        MIXED_MV
    };
    public enum Bundlelosetype : int
    {
        DESTROY,
        SCREEN2D,
        HIDING
    };

    public enum Trackerstatus : int
    {
        LOSE,
        FOUND
    };

    public enum Scanstatus
    {
        /// <summary>
        /// 进入扫描
        /// </summary>
        GOTOSCAN,

        /// <summary>
        /// 停止扫描
        /// </summary>
        STOPSCANNING,

        /// <summary>
        /// 扫描得到结果
        /// </summary>
        SCANTHERESULTS,

        /// <summary>
        /// 扫描完成
        /// </summary>
        SCANISDONE
    };


    public enum Browsertype : int
    {
        ACCESSSQL,
        DOWNLOADFILES
    };

    public enum Viewstatus : int
    {
        HIDE,
        SHOW,
    };

    public enum Getdatas : int
    {
        GETED,
        GETTING
    };

    public enum Takephotoorrecordtype
    {
        NONE,
        TAKEPHOTO,
        RECORD
    };

    public enum Datatype
    {
        INDUSTRY,
        EDUCATION,
        RETAIL,
        GAME,
        OTHER
    }
}