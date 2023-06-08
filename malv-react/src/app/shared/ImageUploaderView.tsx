import {ChangeEvent, Dispatch, useRef, useState} from "react";
import {AsyncFile, fileToDataURL} from "../helpers/FileUtils";
import './ImageUploaderView.css';
import {ImagePickerTabs} from "./ImagePickerTabs";

export interface ImageUploaderViewProps {
    images: AsyncFile[];
    setImages: Dispatch<AsyncFile[]>;
}

export const ImageUploaderView = (props: ImageUploaderViewProps) => {

    const [selectedImage, setSelectedImage] = useState<number>(-1);
    const inputFileRef = useRef(null);

    const uploadFile = async (event: ChangeEvent<HTMLInputElement>) => {
        if (!event.target?.files) return;

        const files: File[] = [];
        for (let i = 0; i < event.target.files.length; i++) {
            files.push(event.target.files[i]);
        }

        Promise.all(files.map(fileToDataURL))
            .then(result => {
                const newImages = props.images.concat(result);
                props.setImages(newImages);
                setSelectedImage(result.length - 1);
            });
    }

    const onFilesReOrder = (files: AsyncFile[]) => {
        const oldSelectedId = props.images[selectedImage].id;
        const newSelectedIndex = files.findIndex(f => f.id === oldSelectedId);
        setSelectedImage(newSelectedIndex);
        props.setImages(files);
    }

    const onSelectedImageChange = (index: number) => {
        setSelectedImage(index);
    }

    return (
        <div className={'w-100'}>
            <div onClick={() => {
                (inputFileRef.current as any).click();
            }}>
                Add
            </div>
            <input id={'adCreateDesc'} type={'file'} ref={inputFileRef} style={{display: "none"}} multiple={true}
                   onChange={(event) => uploadFile(event)}/>
            {selectedImage !== -1 && <img alt={'upload'} src={props.images[selectedImage].url} className={'w-100'}/>}
            <div style={{
                paddingTop: 8,
                paddingBottom: 8
            }}>
                <ImagePickerTabs files={props.images} setFiles={onFilesReOrder} onSelectedImageChange={onSelectedImageChange} />
            </div>
        </div>
    )
}
