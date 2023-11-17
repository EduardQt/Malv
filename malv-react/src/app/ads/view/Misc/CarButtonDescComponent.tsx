import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {IconDefinition} from "@fortawesome/fontawesome-common-types";
import {faCircle} from "@fortawesome/free-solid-svg-icons";

interface CarButtonDescComponentProps {
    icon: IconDefinition;
    title: string;
    desc: string;
}

export const CarButtonDescComponent = ({icon, title, desc}: CarButtonDescComponentProps) => {
    return (
        <div className={'d-inline-block'}>
            <div className={'d-flex text-start'}>
                <span className="fa-stack fa-2x">
                    <FontAwesomeIcon icon={faCircle} className={'fa-stack-2x color-primary'}/>
                    <FontAwesomeIcon icon={icon} className={'fa-stack-1x fa-inverse-2x text-white'}/>
                </span>
                <div>
                    <h4 className={'mb-0'}>{title}</h4>
                    <h3>{desc}</h3>
                </div>
            </div>
        </div>
    )
}
