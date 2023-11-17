import {Col, Row} from "reactstrap";
import {AdImage} from "../../../../network/APIData";
import {Dispatch, useState} from "react";

interface AdGalleryProps {
    adId: number;
    images: string[];
    onClick?: (index: number) => void;
    index: number;
    setIndex: (index: number) => void;
}

export const AdGalleryComponent = (props: AdGalleryProps) => {

    const [index, setIndex] = useState<number>(0)

    const renderImage = () => {
        const url = AdImage(props.adId, props.images[index]);

        return (
            <div style={{cursor: "pointer"}} className={'h-100'} onClick={() => {
                props.setIndex(index);
            }}>
                <img src={url} alt={'Ad id'} className={'h-100 w-100'} style={{
                    objectFit: 'cover'
                }}/>
            </div>
        )
    }

    return (
        <div>
            <Row className={'h-100'}>
                <Col sm={8} md={10} lg={10} className={'p-0'}>
                    {renderImage()}
                </Col>
                <Col sm={4} md={2} lg={2}>
                    Image
                </Col>
            </Row>
        </div>
    )
}
