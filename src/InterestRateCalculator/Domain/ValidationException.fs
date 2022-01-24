namespace Domain

exception ValidationException of message : string
    with
        override this.Message = this.message