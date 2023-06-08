import {Col, Container, Row, Spinner} from "reactstrap";
import {useEffect, useState} from "react";
import {ServiceContainer} from "../../../services/ServiceContainer";
import {Chat_Mod} from "../../../network/Models/Chat_Mod";
import {UserChatComponent} from "./UserChatComponent";

export const MyChatsComponent = () => {

    const [chats, setChats] = useState<Chat_Mod[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [openedChat, setOpenedChat] = useState<number>(-1);

    useEffect(() => {
        const chatService = ServiceContainer.chatService;

        setLoading(true);
        chatService.myChats()
            .then(result => {
                if (result.length > 0)
                    setOpenedChat(result[0].id)
                setChats(result);
                setLoading(false);
            })
            .catch(error => {
                console.log(error);
                setLoading(false);
            });
    }, []);

    const renderLoader = () => {
        return (
            <div className={'justify-content-center align-items-center d-flex h-100'}>
                <Spinner size={"sm"}/>
            </div>
        )
    }

    const renderChats = () => {
        return chats.map((value, index) => {
            return (
                <div role={'button'} onClick={() => setOpenedChat(value.id)} key={index}>
                    <h5>{value.name}</h5>
                </div>
            )
        })
    }

    const renderContent = () => {
        return (
            <div className={'flex-grow-1 d-flex'}>
                <Container>
                    <Row className={'h-100'}>
                        <Col sm={5} md={4} lg={3}>
                            <div className={'h-100'}>
                                {renderChats()}
                            </div>
                        </Col>
                        <Col sm={7} md={8} lg={9}>
                            <UserChatComponent chatId={openedChat}/>
                        </Col>
                    </Row>
                </Container>
            </div>
        )
    }

    return loading ? renderLoader() : renderContent();
}
