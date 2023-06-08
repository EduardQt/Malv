let counter = 0;
export function fileToDataURL(file: File): Promise<AsyncFile> {
    var reader = new FileReader()
    return new Promise(function (resolve, reject) {
        reader.onload = function (event) {
            resolve({
                id: counter++,
                file: file,
                url: event.target?.result as string
            })
        }
        reader.readAsDataURL(file)
    })
}

export interface AsyncFile {
    id: number;
    file: File;
    url: string;
}
