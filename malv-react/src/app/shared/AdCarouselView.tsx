import {Carousel, CarouselControl, CarouselIndicators, CarouselItem} from "reactstrap";
import {useState} from "react";
import {AdImage} from "../../network/APIData";

interface AdCarouselViewProps {
    adId: number;
    images: string[];
}

export const AdCarouselView = (props: AdCarouselViewProps) => {

    const [activeIndex, setActiveIndex] = useState<number>(0);
    const [animating, setAnimating] = useState<boolean>(false);

    const next = () => {
        if (animating) return;
        const nextIndex = activeIndex === props.images.length - 1 ? 0 : activeIndex + 1;
        setActiveIndex(nextIndex);
    }

    const previous = () => {
        if (animating) return;
        const nextIndex = activeIndex === 0 ? props.images.length - 1 : activeIndex - 1;
        setActiveIndex(nextIndex);
    }

    const goToIndex = (newIndex: number) => {
        if (animating) return;
        setActiveIndex(newIndex);
    }

    const slides = props.images.map((item) => {
        const url = AdImage(props.adId, item);
        return (
            <CarouselItem
                onExiting={() => setAnimating(true)}
                onExited={() => setAnimating(false)}
                key={url}
            >
                <img src={url} alt={'Ad id'} className={'h-100 w-100'} style={{
                    objectFit: 'cover'
                }}/>
            </CarouselItem>
        );
    });

    return (
        <Carousel
            activeIndex={activeIndex}
            next={next}
            previous={previous}
            className={'h-100'}
        >
            <CarouselIndicators items={props.images.map(m => {
                return {
                    m
                }
            })} activeIndex={activeIndex} onClickHandler={goToIndex}/>
            {slides}
            <CarouselControl direction="prev" directionText="Previous" onClickHandler={previous}/>
            <CarouselControl direction="next" directionText="Next" onClickHandler={next}/>
        </Carousel>
    );
}
