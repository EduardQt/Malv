import {createRef, useEffect, useState} from "react";
import {ServiceContainer} from "../../../services/ServiceContainer";
import {Chat_Mod} from "../../../network/Models/Chat_Mod";
import {ChatMessage_Mod} from "../../../network/Models/ChatMessage_Mod";
import {Button, Col, Input, InputGroup, Row, Spinner} from "reactstrap";

interface ChatComponentProps {
    chatId: number;
}

export const UserChatComponent = ({chatId}: ChatComponentProps) => {

    const [loading, setLoading] = useState<boolean>(false);
    const [chat, setChat] = useState<Chat_Mod>();
    const [chats, setChats] = useState<ChatMessage_Mod[]>([]);

    const [chatInput, setChatInput] = useState<string>('');
    const [sendChatLoading, setSendChatLoading] = useState<boolean>(false);

    const dummyRef = createRef<HTMLDivElement>();

    useEffect(() => {
        if (chatId === -1) {
            setLoading(false);
            return;
        }

        setLoading(true);
        const chatService = ServiceContainer.chatService;
        chatService.getChat({
            chatId: chatId
        }).then(result => {
            setChat(result.chat);
            setChats(result.chatMessages);
            setLoading(false);
        }).catch(result => {
            setLoading(false);
        });
    }, [chatId]);

    useEffect(() => {
        dummyRef.current?.scrollIntoView();
    }, [chats]);

    const renderLoader = () => {
        return (
            <div className={'justify-content-center align-items-center d-flex h-100'}>
                <Spinner size={"sm"}/>
            </div>
        )
    }

    const renderChats = () => {
        return (
            <div style={{
                flex: '1 1 auto',
                overflowY: 'auto',
                height: '0px'
            }}>
                {chats.filter(f => f).map((value, index) => {
                    return (
                        <div className={'w-100 overflow-hidden mt-1'}>
                            <Col sm={12} md={6} lg={6} className={value.isSelf ? 'float-end' : 'float-start'}>
                                <div style={{
                                    backgroundColor: '#dedede',
                                    display: 'inline-block',
                                    borderRadius: '10px',
                                    padding: '5px',
                                    float: value.isSelf ? 'right' : 'left'
                                }}>
                                    <Row>
                                        <span style={{
                                            fontSize: '12px'
                                        }}>{value.author}</span>
                                        <span style={{
                                            fontSize: '12px'
                                        }}>{new Date(value.sentDate).toDateString()}</span>
                                    </Row>
                                    <span>{value.message}</span>
                                </div>
                            </Col>
                        </div>
                    )
                })}
                <div style={{float: "left", clear: "both"}} ref={dummyRef}>
                </div>
            </div>
        )
    }

    const sendChat = () => {
        const chatService = ServiceContainer.chatService;
        setSendChatLoading(true);
        chatService.sendChatMessage({
            chatId: chatId,
            content: chatInput
        }).then(result => {
            const temp = [...chats, result.chatMessage];
            setChats(temp);
            setSendChatLoading(false);
            setChatInput('');
            dummyRef?.current?.scrollIntoView();
        }).catch(error => {
            setSendChatLoading(false);
        });
    }

    const renderContent = () => {
        return (
            <div style={{
                display: 'flex',
                height: '100%',
                flexDirection: 'column'
            }}>
                <Row>
                    {chat?.name ?? ''}
                </Row>
                {renderChats()}
                <Row className={'p-2'}>
                    {sendChatLoading ? (
                        <div className={'justify-content-center align-items-center d-flex'}>
                            <Spinner size={"sm"}/>
                        </div>
                    ) : (
                        <InputGroup>
                            <Input placeholder={'Message'} value={chatInput}
                                   onChange={(event) => setChatInput(event.target.value)}/>
                            <Button onClick={sendChat}>Send</Button>
                        </InputGroup>
                    )}
                </Row>
            </div>
        )
    }

    return loading ? renderLoader() : renderContent();
}
