A fork of CjangCjengh's NovelpiaDownloader that grabs tags, author name, synopsis and adds them to epub metadata. Along with better epub formatting, including html tags and newlines support.

<BR>

And now with support for [Novelpia Library Manager](https://github.com/SpazzTL/Novelpia-Library-Manager) for bulk (re)downloading


# NovelpiaDownloader Command-Line Arguments

The NovelpiaDownloader can be operated directly from the command line, allowing for automated and scripted downloads. The following arguments can be used to control its behavior without interacting with the graphical user interface.

---

### General Arguments

| Argument          | Value Type  | Description                                                                                                                   |
| :---------------- | :---------- | :---------------------------------------------------------------------------------------------------------------------------- |
| **`-novelid`** | `number`    | **Required.** The ID of the novel you wish to download. This can be found in the novel's URL on the Novelpia website.         |
| **`-output`** | `file path` | Specifies the full path, including the desired filename, for the output file. The file extension is overridden by format flags. |
| **`-from`** | `number`    | Optional. The chapter number to start the download from (inclusive). If omitted, it will start from the first chapter.         |
| **`-to`** | `number`    | Optional. The chapter number to end the download at (inclusive). If omitted, it will download to the very last chapter.       |

---

### Output Format Arguments

Use one of the following flags to set the output format. If no format argument is specified, the output will default to a plain `.txt` file.

| Argument | Value Type | Description                                                                                                           |
| :------- | :--------- | :-------------------------------------------------------------------------------------------------------------------- |
| **`-epub`** | `none`     | Saves the novel as an `.epub` file. This format includes metadata, the cover image, and embedded chapter images.      |
| **`-html`** | `none`     | Saves the novel as a single, self-contained `.html` file. This format includes metadata and images. |

---

### Image-Related Arguments

These arguments control how images are handled in `.epub` and `.html` formats.

| Argument            | Value Type | Description                                                                                                                    |
| :------------------ | :--------- | :----------------------------------------------------------------------------------------------------------------------------- |
| **`-compressimages`** | `none`     | Enables JPEG compression for all downloaded images (cover and chapters) to reduce the final file size.                         |
| **`-jpegquality`** | `number`   | Used with `-compressimages`. Sets the quality of the JPEG compression from 0 to 100. The default is 80. |

---

### Batch Downloading Arguments

These arguments are used to download multiple novels from a list.

| Argument        | Value Type       | Description                                                                                                       |
| :-------------- | :--------------- | :---------------------------------------------------------------------------------------------------------------- |
| **`-listfile`** | `file path`      | Specifies the path to a `.txt` file containing the novels to download. Each line must be in `Novel Title,Novel ID` format. |
| **`-outputdir`** | `directory path` | Specifies the folder where all novels from the batch download will be saved.                                      |

---

### Usage Examples

#### 1. Basic Download
Download a novel as a default `.txt` file.
```bash
NovelpiaDownloader.exe -novelid 123456 -output "C:\Downloads\FantasyNovel.txt"
```

#### 2. EPUB Download with Image Compression
Download a novel as an `.epub`, with images compressed to 75% quality.
```bash
NovelpiaDownloader.exe -novelid 123456 -output "C:\Downloads\FantasyNovel.epub" -epub -compressimages -jpegquality 75
```

#### 3. HTML Download
Download a novel as a single `.html` file.
```bash
NovelpiaDownloader.exe -novelid 123456 -output "C:\Downloads\FantasyNovel.html" -html
```

#### 4. Partial Download
Download only chapters 50 through 100 of a novel as an `.epub`.
```bash
NovelpiaDownloader.exe -novelid 123456 -output "C:\Downloads\FantasyNovel_chapters_50-100.epub" -epub -from 50 -to 100
```

#### 5. Batch Download
Download all novels listed in `MyList.txt` and save them as `.epub` files in the `C:\NovelCollection` folder.

* **Contents of `MyList.txt`:**
    ```
    Novel1,123456
    Another Story,789012
    ```
* **Command:**
    ```bash
    NovelpiaDownloader.exe -listfile "C:\Users\Admin\Desktop\MyList.txt" -outputdir "C:\NovelCollection" -epub
    ```
