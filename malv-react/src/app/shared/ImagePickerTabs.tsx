import React from "react";
import {DragDropContext, Droppable, Draggable} from "react-beautiful-dnd";
import {AsyncFile} from "../helpers/FileUtils";

// a little function to help us with reordering the result
const reorder = (list: any, startIndex: any, endIndex: any) => {
    const result = Array.from(list);
    const [removed] = result.splice(startIndex, 1);
    result.splice(endIndex, 0, removed);

    return result;
};

const getItemStyle = (isDragging: any, draggableStyle: any) => ({
    // some basic styles to make the items look a bit nicer
    userSelect: "none",
    width: '96px',
    height: '96px',
    margin: `0 8px 0 0`,

    // change background colour if dragging
    background: isDragging ? "lightgreen" : "grey",

    // styles we need to apply on draggables
    ...draggableStyle
});

interface ImagePickerProps {
    files: AsyncFile[];
    setFiles: (files: AsyncFile[]) => void;
    onSelectedImageChange: (index: number) => void;
}

export const ImagePickerTabs = (props: ImagePickerProps) => {
    const onDragEnd = (result: any) => {
        // dropped outside the list
        if (!result.destination) {
            return;
        }

        const tempItems = reorder(
            props.files,
            result.source.index,
            result.destination.index
        );

        props.setFiles(tempItems as any);
    }

    return (
        <DragDropContext onDragEnd={onDragEnd}>
            <Droppable droppableId="droppable" direction="horizontal">
                {(provided: any, snapshot: any) => (
                    <div
                        ref={provided.innerRef}
                        style={{
                            display: "flex",
                            overflow: "auto",
                            height: '112px'
                        }}
                        {...provided.droppableProps}
                    >
                        {props.files.map((item, index) => (
                            <Draggable key={item.id} draggableId={item.id.toString()} index={index}>
                                {(provided, snapshot) => (
                                    <div
                                        ref={provided.innerRef}
                                        {...provided.draggableProps}
                                        {...provided.dragHandleProps}
                                        style={getItemStyle(
                                            snapshot.isDragging,
                                            provided.draggableProps.style
                                        )}
                                        onClick={() => props.onSelectedImageChange(index)}
                                        className={"div-entry-test"}
                                    >
                                        <img alt={'picker-tab'} src={item.url} style={{
                                            width: '96px',
                                            height: '96px',
                                            objectFit: 'cover'
                                        }}/>
                                    </div>
                                )}
                            </Draggable>
                        ))}
                    </div>
                )}
            </Droppable>
        </DragDropContext>
    );
}
